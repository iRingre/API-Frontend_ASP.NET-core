using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FrontEnd.Components.Pages
{
    public partial class Counter
    {
        private int currentCount = 0;
        //utilizza mad blazor
        protected override void OnInitialized() => currentCount = initialCount;

        string text = "";
        string divText = "Banana";
        string SearchResults = "";
        [Parameter]
        public int initialCount { get; set; }

        [Parameter]
        public int IncrementAmmount { get; set; } = 1;
        private void IncrementCount() => currentCount += IncrementAmmount;
        private void IncrementCount2(MouseEventArgs e) => currentCount++;
        //private void OnInput(ChangeEventArgs args) => text = (string)args.Value;
        private void MouseOver(MouseEventArgs args) => divText = "Mouse Sopra il Div";
        private void MouseOut(MouseEventArgs args) => divText = "Banana";
        private void OnClick(MouseEventArgs args) => text = "";
        async Task Search()
        {
            SearchResults = "Searching...";
            await Task.Delay(2000);
            SearchResults = $"Found {Random.Shared.Next()} results!";
        }
    }
}