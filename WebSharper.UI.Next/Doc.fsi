// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2014 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}

namespace WebSharper.UI.Next

/// Represents a time-varying node or a node list.
[<Interface>]
type Doc =
    inherit WebSharper.Html.Client.IControlBody
    abstract ToDynDoc : DynDoc

and [<Sealed>] DynDoc =
    interface Doc

[<Sealed>]
type Elt =
    interface Doc

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Doc =

    open Microsoft.FSharp.Quotations
    open WebSharper.Html.Client

    // Construction of basic nodes.

    /// Constructs a reactive element node.
    val Element : name: string -> seq<Attr> -> seq<Doc> -> Elt

    /// Same as Element, but uses SVG namespace.
    val SvgElement : name: string -> seq<Attr> -> seq<Doc> -> Elt

    // Note: Empty, Append, Concat define a monoid on Doc.

    /// Empty tree.
    val Empty : Doc

    /// Append on trees.
    val Append : Doc -> Doc -> Doc

    /// Concatenation.
    val Concat : seq<Doc> -> Doc

    // Special cases

    /// Static variant of TextView.
    val TextNode : string -> Doc

    val ClientSide : Expr<#IControlBody> -> Doc

namespace WebSharper.UI.Next.Server

open WebSharper.UI.Next

module Doc =
    open WebSharper.Html.Server

    /// Converts a `Doc` to a list of sitelet elements.
    val AsElements : Doc -> list<Html.Element>

[<AutoOpen>]
module Extensions =
    open WebSharper.Sitelets

    type Content<'Action> with
        /// Converts a `Doc` to a sitelet Page.
        /// `Doc` values that correspond to HTML fragements are converted to full documents.
        static member Doc : Doc -> Async<Content<'Action>>

        /// Creates an HTML page response from `Doc`s.
        static member Doc
            : ?Body: #seq<Doc>
            * ?Head: #seq<Doc>
            * ?Title: string
            * ?Doctype: string
            -> Async<Content<'EndPoint>>

namespace WebSharper.UI.Next.Client

open WebSharper.JavaScript
open WebSharper.UI.Next

[<AutoOpen>]
module EltExtensions =

    type Elt with

        /// Get the underlying DOM element.
        member Dom : Dom.Element

        /// Add an event handler.
        member On : event: string * callback: (Dom.Element -> Dom.Event -> unit) -> Elt

        // {{ event
        /// Add a handler for the event "abort".
        member OnAbort : cb: (Dom.Element -> Dom.UIEvent -> unit) -> Elt
        /// Add a handler for the event "afterprint".
        member OnAfterPrint : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "animationend".
        member OnAnimationEnd : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "animationiteration".
        member OnAnimationIteration : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "animationstart".
        member OnAnimationStart : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "audioprocess".
        member OnAudioProcess : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "beforeprint".
        member OnBeforePrint : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "beforeunload".
        member OnBeforeUnload : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "beginEvent".
        member OnBeginEvent : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "blocked".
        member OnBlocked : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "blur".
        member OnBlur : cb: (Dom.Element -> Dom.FocusEvent -> unit) -> Elt
        /// Add a handler for the event "cached".
        member OnCached : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "canplay".
        member OnCanPlay : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "canplaythrough".
        member OnCanPlayThrough : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "change".
        member OnChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "chargingchange".
        member OnChargingChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "chargingtimechange".
        member OnChargingTimeChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "checking".
        member OnChecking : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "click".
        member OnClick : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "close".
        member OnClose : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "complete".
        member OnComplete : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "compositionend".
        member OnCompositionEnd : cb: (Dom.Element -> Dom.CompositionEvent -> unit) -> Elt
        /// Add a handler for the event "compositionstart".
        member OnCompositionStart : cb: (Dom.Element -> Dom.CompositionEvent -> unit) -> Elt
        /// Add a handler for the event "compositionupdate".
        member OnCompositionUpdate : cb: (Dom.Element -> Dom.CompositionEvent -> unit) -> Elt
        /// Add a handler for the event "contextmenu".
        member OnContextMenu : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "copy".
        member OnCopy : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "cut".
        member OnCut : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "dblclick".
        member OnDblClick : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "devicelight".
        member OnDeviceLight : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "devicemotion".
        member OnDeviceMotion : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "deviceorientation".
        member OnDeviceOrientation : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "deviceproximity".
        member OnDeviceProximity : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "dischargingtimechange".
        member OnDischargingTimeChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "DOMActivate".
        member OnDOMActivate : cb: (Dom.Element -> Dom.UIEvent -> unit) -> Elt
        /// Add a handler for the event "DOMAttributeNameChanged".
        member OnDOMAttributeNameChanged : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "DOMAttrModified".
        member OnDOMAttrModified : cb: (Dom.Element -> Dom.MutationEvent -> unit) -> Elt
        /// Add a handler for the event "DOMCharacterDataModified".
        member OnDOMCharacterDataModified : cb: (Dom.Element -> Dom.MutationEvent -> unit) -> Elt
        /// Add a handler for the event "DOMContentLoaded".
        member OnDOMContentLoaded : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "DOMElementNameChanged".
        member OnDOMElementNameChanged : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "DOMNodeInserted".
        member OnDOMNodeInserted : cb: (Dom.Element -> Dom.MutationEvent -> unit) -> Elt
        /// Add a handler for the event "DOMNodeInsertedIntoDocument".
        member OnDOMNodeInsertedIntoDocument : cb: (Dom.Element -> Dom.MutationEvent -> unit) -> Elt
        /// Add a handler for the event "DOMNodeRemoved".
        member OnDOMNodeRemoved : cb: (Dom.Element -> Dom.MutationEvent -> unit) -> Elt
        /// Add a handler for the event "DOMNodeRemovedFromDocument".
        member OnDOMNodeRemovedFromDocument : cb: (Dom.Element -> Dom.MutationEvent -> unit) -> Elt
        /// Add a handler for the event "DOMSubtreeModified".
        member OnDOMSubtreeModified : cb: (Dom.Element -> Dom.MutationEvent -> unit) -> Elt
        /// Add a handler for the event "downloading".
        member OnDownloading : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "drag".
        member OnDrag : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "dragend".
        member OnDragEnd : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "dragenter".
        member OnDragEnter : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "dragleave".
        member OnDragLeave : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "dragover".
        member OnDragOver : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "dragstart".
        member OnDragStart : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "drop".
        member OnDrop : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "durationchange".
        member OnDurationChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "emptied".
        member OnEmptied : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "ended".
        member OnEnded : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "endEvent".
        member OnEndEvent : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "error".
        member OnError : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "focus".
        member OnFocus : cb: (Dom.Element -> Dom.FocusEvent -> unit) -> Elt
        /// Add a handler for the event "fullscreenchange".
        member OnFullScreenChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "fullscreenerror".
        member OnFullScreenError : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "gamepadconnected".
        member OnGamepadConnected : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "gamepaddisconnected".
        member OnGamepadDisconnected : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "hashchange".
        member OnHashChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "input".
        member OnInput : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "invalid".
        member OnInvalid : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "keydown".
        member OnKeyDown : cb: (Dom.Element -> Dom.KeyboardEvent -> unit) -> Elt
        /// Add a handler for the event "keypress".
        member OnKeyPress : cb: (Dom.Element -> Dom.KeyboardEvent -> unit) -> Elt
        /// Add a handler for the event "keyup".
        member OnkeyUp : cb: (Dom.Element -> Dom.KeyboardEvent -> unit) -> Elt
        /// Add a handler for the event "languagechange".
        member OnLanguageChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "levelchange".
        member OnLevelChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "load".
        member OnLoad : cb: (Dom.Element -> Dom.UIEvent -> unit) -> Elt
        /// Add a handler for the event "loadeddata".
        member OnLoadedData : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "loadedmetadata".
        member OnLoadedMetadata : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "loadend".
        member OnLoadEnd : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "loadstart".
        member OnLoadStart : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "message".
        member OnMessage : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "mousedown".
        member OnMouseDown : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "mouseenter".
        member OnMouseEnter : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "mouseleave".
        member OnMouseLeave : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "mousemove".
        member OnMouseMove : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "mouseout".
        member OnMouseOut : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "mouseover".
        member OnMouseOver : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "mouseup".
        member OnMouseUp : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "noupdate".
        member OnNoUpdate : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "obsolete".
        member OnObsolete : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "offline".
        member OnOffline : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "online".
        member OnOnline : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "open".
        member OnOpen : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "orientationchange".
        member OnOrientationChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "pagehide".
        member OnPageHide : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "pageshow".
        member OnPageShow : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "paste".
        member OnPaste : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "pause".
        member OnPause : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "play".
        member OnPlay : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "playing".
        member OnPlaying : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "pointerlockchange".
        member OnPointerLockChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "pointerlockerror".
        member OnPointerLockError : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "popstate".
        member OnPopState : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "progress".
        member OnProgress : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "ratechange".
        member OnRateChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "readystatechange".
        member OnReadyStateChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "repeatEvent".
        member OnRepeatEvent : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "reset".
        member OnReset : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "resize".
        member OnResize : cb: (Dom.Element -> Dom.UIEvent -> unit) -> Elt
        /// Add a handler for the event "scroll".
        member OnScroll : cb: (Dom.Element -> Dom.UIEvent -> unit) -> Elt
        /// Add a handler for the event "seeked".
        member OnSeeked : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "seeking".
        member OnSeeking : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "select".
        member OnSelect : cb: (Dom.Element -> Dom.UIEvent -> unit) -> Elt
        /// Add a handler for the event "show".
        member OnShow : cb: (Dom.Element -> Dom.MouseEvent -> unit) -> Elt
        /// Add a handler for the event "stalled".
        member OnStalled : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "storage".
        member OnStorage : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "submit".
        member OnSubmit : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "success".
        member OnSuccess : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "suspend".
        member OnSuspend : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "SVGAbort".
        member OnSVGAbort : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "SVGError".
        member OnSVGError : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "SVGLoad".
        member OnSVGLoad : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "SVGResize".
        member OnSVGResize : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "SVGScroll".
        member OnSVGScroll : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "SVGUnload".
        member OnSVGUnload : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "SVGZoom".
        member OnSVGZoom : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "timeout".
        member OnTimeOut : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "timeupdate".
        member OnTimeUpdate : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "touchcancel".
        member OnTouchCancel : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "touchend".
        member OnTouchEnd : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "touchenter".
        member OnTouchEnter : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "touchleave".
        member OnTouchLeave : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "touchmove".
        member OnTouchMove : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "touchstart".
        member OnTouchStart : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "transitionend".
        member OnTransitionEnd : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "unload".
        member OnUnload : cb: (Dom.Element -> Dom.UIEvent -> unit) -> Elt
        /// Add a handler for the event "updateready".
        member OnUpdateReady : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "upgradeneeded".
        member OnUpgradeNeeded : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "userproximity".
        member OnUserProximity : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "versionchange".
        member OnVersionChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "visibilitychange".
        member OnVisibilityChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "volumechange".
        member OnVolumeChange : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "waiting".
        member OnWaiting : cb: (Dom.Element -> Dom.Event -> unit) -> Elt
        /// Add a handler for the event "wheel".
        member OnWheel : cb: (Dom.Element -> Dom.WheelEvent -> unit) -> Elt
        // }}

        /// Add the given doc as first child(ren) of this element.
        member Prepend : Doc -> unit

        /// Add the given doc as last child(ren) of this element.
        member Append : Doc -> unit

        /// Remove all children from the element.
        member Clear : unit -> unit

        /// Get the HTML string for this element in its current state.
        member Html : string

        /// Get the element's id.
        member Id : string

        /// Get or set the element's current value.
        member Value : string with get, set

        /// Get or set the element's text content.
        member Text : string with get, set

        /// Get the given attribute's value.
        member GetAttribute : name: string -> string

        /// Set the given attribute's value.
        member SetAttribute : name: string * value: string -> unit

        /// Checks whether the element has the given attribute.
        member HasAttribute : name: string -> bool

        /// Unsets the given attribute.
        member RemoveAttribute : name: string -> unit

        /// Get the given property's value.
        member GetProperty : name: string -> 'T

        /// Set the given property's value.
        member SetProperty : name: string * value: 'T -> unit

        /// Add a CSS class to the element.
        member AddClass : ``class``: string -> unit

        /// Remove a CSS class from the element.
        member RemoveClass : ``class``: string -> unit

        /// Checks whether the element has a CSS class.
        member HasClass : ``class``: string -> bool

        /// Sets an inline style.
        member SetStyle : name: string * value: string -> unit

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Doc =

  // Construction of basic nodes.

    /// Embeds time-varying fragments.
    val EmbedView : View<#Doc> -> Doc

    /// Embeds time-varying fragments.
    /// Equivalent to View.Map followed by Doc.EmbedView.
    val BindView : ('T -> #Doc) -> View<'T> -> Doc

    /// Creates a Doc using a given DOM element
    val Static : Element -> Elt

    /// Constructs a reactive text node.
    val TextView : View<string> -> Doc

  // Collections.

    /// Converts a collection to Doc using View.Convert and embeds the concatenated result.
    /// Shorthand for View.Convert f |> View.Map Doc.Concat |> Doc.EmbedView.
    val Convert : ('T -> #Doc) -> View<seq<'T>> -> Doc
        when 'T : equality

    /// Doc.Convert with a custom key.
    val ConvertBy : ('T -> 'K) -> ('T -> #Doc) -> View<seq<'T>> -> Doc
        when 'K : equality

    /// Converts a collection to Doc using View.ConvertSeq and embeds the concatenated result.
    /// Shorthand for View.ConvertSeq f |> View.Map Doc.Concat |> Doc.EmbedView.
    val ConvertSeq : (View<'T> -> #Doc) -> View<seq<'T>> -> Doc
        when 'T : equality

    /// Doc.ConvertSeq with a custom key.
    val ConvertSeqBy : ('T -> 'K) -> (View<'T> -> #Doc) -> View<seq<'T>> -> Doc
        when 'K : equality

  // Main entry-point combinators - use once per app

    /// Runs a reactive Doc as contents of the given element.
    val Run : Element -> Doc -> unit

    /// Same as rn, but looks up the element by ID.
    val RunById : id: string -> Doc -> unit

    /// Creates a Pagelet from a Doc, in a Div container.
    val AsPagelet : Doc -> WebSharper.Html.Client.Pagelet

  // Form helpers

    /// Input box.
    val Input : seq<Attr> -> Var<string> -> Elt

    /// Input box with type="number".
    val IntInput : seq<Attr> -> Var<int> -> Elt

    /// Input box with type="number".
    val FloatInput : seq<Attr> -> Var<float> -> Elt

    /// Input text area.
    val InputArea : seq<Attr> -> Var<string> -> Elt

    /// Password box.
    val PasswordBox : seq<Attr> -> Var<string> -> Elt

    /// Submit button. Calls the callback function when the button is pressed.
    val Button : caption: string -> seq<Attr> -> (unit -> unit) -> Elt

    /// Submit button. Takes a view of reactive components with which it is associated,
    /// and a callback function of what to do with this view once the button is pressed.
    val ButtonView : caption: string -> seq<Attr> -> View<'T> -> ('T-> unit) -> Elt

    /// Hyperlink. Calls the callback function when the link is clicked.
    val Link : caption: string -> seq<Attr> -> (unit -> unit) -> Elt

    /// Hyperlink. Takes a view of reactive components with which it is associated,
    /// and a callback function of what to do with this view once the link is clicked.
    val LinkView : caption: string -> seq<Attr> -> View<'T> -> ('T -> unit) -> Elt

    /// Check Box.
    val CheckBox : seq<Attr> -> Var<bool> -> Elt

    /// Check Box Group.
    val CheckBoxGroup : seq<Attr> -> 'T -> Var<list<'T>> -> Elt
        when 'T : equality

    /// Select box.
    val Select : seq<Attr> -> ('T -> string) -> list<'T> -> Var<'T> -> Elt
        when 'T : equality

    /// Radio button.
    val Radio : seq<Attr> -> 'T -> Var<'T> -> Elt
        when 'T : equality
