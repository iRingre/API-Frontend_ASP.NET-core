using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FrontEnd.Components
{
    public partial class MyButton
    {
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public RenderFragment ChildContent {get; set;}

        [Parameter(CaptureUnmatchedValues = true)]//permette di catturare altri tipi che non sarebbero normalmente accettati
        public Dictionary<string, object> AdditionsalAttributes {get; set;}
        //questa funzione serviva per prendere e passare l'oggetto OnClick però la chiamata 
        //@onclick prende oggetti del tipo EventCallback quindi gli passo direttamente l'oggetto OnClick
        /*private Task OnButtonClick(MouseEventArgs args)
        {
           return OnClick.InvokeAsync(args);
        }*/
    }
}