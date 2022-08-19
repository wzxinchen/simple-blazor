var dispatcherWindowEvent = Module.mono_bind_static_method("[SimpleBlazor.JavaScript] SimpleBlazor.JavaScript.Window:DispatchWindowEvent");
location.hash = "";
window.addEventListener('hashchange', (win, e) => {
    dispatcherWindowEvent('hashchange');
});
var dispatchClickEvent = Module.mono_bind_static_method("[SimpleBlazor.JavaScript] SimpleBlazor.JavaScript.Element:DispatchClickEvent");
function onclickHandler(handler) {
    dispatchClickEvent(handler);
}