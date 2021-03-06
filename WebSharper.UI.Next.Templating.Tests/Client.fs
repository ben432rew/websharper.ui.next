namespace WebSharper.UI.Next.Tests

open WebSharper
open WebSharper.JavaScript

open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Notation
open WebSharper.UI.Next.Templating

[<JavaScript>]
module Client =    
    open WebSharper.UI.Next.Client

    type MyTemplate = Template<"template.html">

    type Item =
        { id : int; name: string; description: string }
        static member Key x = x.id

#if ZAFIR
    [<SPAEntryPoint>]
    let Main() =
#else
    let Main =
#endif
        let myItems =
            ListModel.CreateWithStorage Item.Key (Storage.LocalStorage "Test" Serializer.Default)

        let newName = Var.Create ""
        let newDescr = Var.Create ""
        let itemsSub = Submitter.Create myItems.View Seq.empty
        let stitle = "Starting titlo"
        let var = Var.Create ""

        let title = 
            stitle
            |> Seq.toList
            |> List.map Var.Create

        async {
            do! Async.Sleep 1500
            Var.Set (List.nth title (title.Length - 1)) 'e'
        } |> Async.Start

        let tv = title
                 |> Seq.map View.FromVar
                 |> View.Sequence
                 |> View.Map (fun e -> new string(Seq.toArray e))
        let btnSub = Submitter.Create var.View ""
 
        let mutable lastKey = myItems.Length
        let freshKey() =
            lastKey <- lastKey + 1
            lastKey

        let findByKey = Var.Create ""
        let found = 
            findByKey.View.BindInner(fun s -> 
                myItems.TryFindByKeyAsView(int s).Map(function 
                    | None -> "none" 
                    | Some a -> a.name + ":" + a.description))

        let chk = Var.Create true

        let eltUpdater = (div []).ToUpdater()
        let testCounter = Var.Create 0
        let testCounterStr = testCounter.View.Map(string)
        let added = System.Collections.Generic.Queue<Elt>()
        let removed = System.Collections.Generic.Queue<Elt>()

        let addDiv () =
            let child =
                div [ textView testCounterStr ]  
            added.Enqueue child
            eltUpdater.Dom.AppendChild(child.Dom) |> ignore
            eltUpdater.AddUpdated child

        let removeUpdater () =
            if added.Count > 0 then
                let rem = added.Dequeue()
                eltUpdater.RemoveUpdated rem
                removed.Enqueue rem

        let reAddUpdater () =
            if removed.Count > 0 then
                let readd = removed.Dequeue()
                eltUpdater.AddUpdated readd
                added.Enqueue readd

        let doc =
            MyTemplate()
                .Attr(Attr.Style "font-weight" "bold")
                .Title(
                    h1Attr [
                        attr.style "color: blue"
                        attr.classDynPred var.View (View.Const true)
                        on.click (fun el ev -> Console.Log ev)
                    ] [textView tv]
                )
                .ListContainer(
                    myItems.ViewState.DocSeqCached(Item.Key, fun key item ->
                        MyTemplate.ListItem()
                            .Key(item.Map(fun i -> string i.id))
                            .Name(item.Map(fun i -> i.name))
                            .Description(myItems.LensInto (fun i -> i.description) (fun i d -> { i with description = d }) key)
                            .FontStyle("italic")
                            .FontWeight("bold")
                            .Remove(fun _ _ -> myItems.RemoveByKey key)
                            .Elt()
                            .OnClickView(item, fun _ _ x -> JS.Alert x.name)
                    )
                )
                .NewName(newName)
                .NewDescription(newDescr)
                .NewItem(fun () -> myItems.Add { id = freshKey(); name = newName.Value; description = newDescr.Value })
                .SubmitItems(itemsSub.Trigger)
                .ClearItems(myItems.Clear)
                .LeaveThisEmpty(
                    // Test #102: this would empty the whole containing div
                    View.Const Doc.Empty |> Doc.EmbedView
                )
                .FindBy(findByKey)
                .Found(found)
                .Length(myItems.ViewState.Map(fun s -> printfn "mapping length"; string s.Length))
                .Names(
                    myItems.ViewState.Map(fun s -> 
                        s.ToArray(fun i -> not (System.String.IsNullOrEmpty i.description))
                        |> Seq.map (fun i -> i.name)
                        |> String.concat ", "
                    )
                )
                .ListView(
                    itemsSub.View.DocSeqCached(Item.Key, fun key item ->
                        MyTemplate.ListViewItem()
                            .Name(item.Map(fun i -> i.name))
                            .Description(item.Map(fun i -> i.description))
                            .Doc()
                    )
                )
                .MyInput(var)
                .MyInputView(btnSub.View)
                .MyCallback(btnSub.Trigger)
                .ButtonExtraText(" now")
                .Checked(chk)
                .IsChecked(chk.View.Map(function true -> "checked" | false -> "not checked"))
                .NameChanged(fun el ev -> 
                   let key = if ev?which then ev?which else ev?keyCode
                   if key = 13 then newName := "")
                .PRendered(fun (el: Dom.Element) -> var := el.GetAttribute("id"))
                .ControlTests(
                    let clk = Var.Create ""
                    let chk = Var.Create true
                    let chkl = Var.Create [ 2 ]
                    let inp = Var.Create "hello"
                    let iinp = Var.Create (CheckedInput.Make 42)
                    let ri = Var.Create 0
                    [ 
                        p [
                            Doc.Button "Click me" [] (fun () -> clk := "Clicked!")
                            textView clk.View
                        ] :> Doc
                        p [
                            Doc.CheckBox [] chk 
                            textView (chk.View.Map(function false -> "Check this" | true -> "Uncheck this"))
                        ] :> Doc
                        p [
                            for i in 1 .. 5 ->
                                Doc.CheckBoxGroup [] i chkl :> Doc 
                            yield textView (chkl.View.Map(fun l -> "Checked indices:" + (l |> List.map string |> String.concat ", ")))
                        ] :> Doc
                        p [
                            Doc.Input [] inp 
                            textView (inp.View.Map(fun s -> "You said: " + s))
                        ] :> Doc
                        p [
                            Doc.IntInput [] iinp 
                            textView (iinp.View.Map(function Valid (i, _) -> "It's an int: " + string i | Invalid _ -> "Can't parse" | Blank _ -> "Empty" ))
                        ] :> Doc
                        p [
                            for i in 1 .. 5 ->
                                Doc.Radio [] i ri :> Doc 
                            yield textView (ri.View.Map(fun i -> "Checked index:" + string i))
                        ] :> Doc
                    ]
                )
                .AddDiv(fun _ _ -> addDiv())
                .RemoveUpdater(fun _ _ -> removeUpdater())
                .ReAddUpdater(fun _ _ -> reAddUpdater())
                .IncrEltUpdaterTest(fun _ _ -> testCounter := !testCounter + 1)
                .EltUpdaterTest(eltUpdater)
                .Doc()

        Anim.UseAnimations <- false

        div [
            doc 
            Regression67.Doc
        ]
        |> Doc.RunById "main"

        Console.Log("Running JavaScript Entry Point..")
