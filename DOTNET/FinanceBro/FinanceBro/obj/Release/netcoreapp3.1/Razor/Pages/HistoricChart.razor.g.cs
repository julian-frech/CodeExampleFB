#pragma checksum "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c0541ac0f1058574dea52e6e804b9e39ba8710bf"
// <auto-generated/>
#pragma warning disable 1591
namespace FinanceBro.Pages
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using FinanceBro;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Blazorise.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Blazorise.Icons.FontAwesome;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using FinanceBro.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using BlazorTable;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Syncfusion.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Syncfusion.Blazor.Layouts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Syncfusion.Blazor.Maps;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Syncfusion.Blazor.Notifications;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/_Imports.razor"
using Microsoft.Extensions.Logging;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
using FinanceBro.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
using Blazorise.Charts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
using Blazorise;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
using System;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/historicChart")]
    public partial class HistoricChart : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __Blazor.FinanceBro.Pages.HistoricChart.TypeInference.CreateAutocomplete_0(__builder, 0, 1, 
#nullable restore
#line 19 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                     symbolFactsDataList

#line default
#line hidden
#nullable disable
            , 2, 
#nullable restore
#line 20 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                           (item)=>item.CompanyName

#line default
#line hidden
#nullable disable
            , 3, 
#nullable restore
#line 21 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                             (item)=> item.Symbol

#line default
#line hidden
#nullable disable
            , 4, 
#nullable restore
#line 22 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                              selectedListValue

#line default
#line hidden
#nullable disable
            , 5, Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Object>(this, 
#nullable restore
#line 23 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                                     SymbolSelectionListHandler

#line default
#line hidden
#nullable disable
            ), 6, "Company Name ...");
            __builder.AddMarkupContent(7, "\n\n");
            __builder.OpenElement(8, "form");
            __builder.AddAttribute(9, "class", "form-inline");
            __builder.OpenElement(10, "div");
            __builder.AddAttribute(11, "class", "input-group mb-3");
            __builder.AddMarkupContent(12, "<div class=\"input-group-prepend\"><span class=\"input-group-text\" id=\"basic-addon3\">Days Interval:</span></div>\n        ");
            __builder.OpenElement(13, "input");
            __builder.AddAttribute(14, "id", "Interval1");
            __builder.AddAttribute(15, "type", "text");
            __builder.AddAttribute(16, "placeholder", "(Days)" + (
#nullable restore
#line 31 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                                                              TimeInterval

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 31 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                                                                                          TimeInterval

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(18, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => TimeInterval = __value, TimeInterval));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\n    ");
            __builder.OpenElement(20, "input");
            __builder.AddAttribute(21, "class", "btn btn-primary mb-3");
            __builder.AddAttribute(22, "type", "button");
            __builder.AddAttribute(23, "value", "Confirm");
            __builder.AddAttribute(24, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 33 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                                                                                  () => SymbolSelectionListHandler(null)

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\n\n");
            __builder.OpenComponent<Blazorise.Slider<int>>(26);
            __builder.AddAttribute(27, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<int>(
#nullable restore
#line 36 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                             TimeInterval

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(28, "Max", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<int>(
#nullable restore
#line 36 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                                                365

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(29, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<int>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<int>(this, 
#nullable restore
#line 36 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                                                                      (int v) => IntervalSelectionHandler(v) 

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
            __builder.AddMarkupContent(30, "\n\n");
            __builder.OpenComponent<Blazorise.Charts.LineChart<double>>(31);
            __builder.AddComponentReferenceCapture(32, (__value) => {
#nullable restore
#line 38 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
                 lineChart = (Blazorise.Charts.LineChart<double>)__value;

#line default
#line hidden
#nullable disable
            }
            );
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 40 "/Users/julianfrech/Documents/CentralFinanceManagerV1/DOTNET/FinanceBro/FinanceBro/Pages/HistoricChart.razor"
      

    public List<SymbolFacts> symbolFactsDataList { get; set; }
    private int TimeInterval { get; set; } = 14;
    object selectedListValue { get; set; }
    LineChart<double> lineChart;
    List<MarketData> marketDataList;
    List<string> SymbolSelections = new List<string>();
    List<string>
backgroundColors = new List<string>
{ ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };

    List<string>
        borderColors = new List<string>
            { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

    /// <summary>
    /// Method triggered on page initialization.
    /// sets list of all possible symbols.
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        symbolFactsDataList = await SymbolListService.Get();
        StateHasChanged();
    }




    /// <summary>
    /// Method to ammend new symbols to the list SymbolSelections.
    /// After the selection based on the user input i
    /// </summary>
    /// <param name="SymbolSelectionFromUser"></param>
    private async Task SymbolSelectionListHandler(object SymbolSelectionFromUser)
    {
        if (!(SymbolSelectionFromUser is null))
        {
            if (!SymbolSelections.Contains(SymbolSelectionFromUser.ToString().ToLower()))
            {
                if (SymbolSelections.Count() > 3) { SymbolSelections.RemoveAt(0); }
                SymbolSelections.Add(SymbolSelectionFromUser.ToString().ToLower());
            }

            selectedListValue = SymbolSelectionFromUser;
            StateHasChanged();
        }
        await ChartDataRanderHandler();
    }

    async Task ChartDataRanderHandler()
    {
        await lineChart.Clear();

        await GetMarketData();

        await lineChart.AddLabel(marketDataList.Where(x => x.MarketTimestamp.Hour == 0 && x.MarketTimestamp.Minute == 0 && x.MarketTimestamp > DateTime.Today.AddDays(-TimeInterval)).OrderBy(x => x.MarketTimestamp).Select(x => x.MarketTimestamp.ToShortDateString()).Distinct().ToArray());

        foreach (var item in SymbolSelections)
        {
            await lineChart.AddDatasetsAndUpdate(GetLineChartDataset(item));
        }

    }

    private async Task IntervalSelectionHandler(int _IntervalUserInput)
    {
        this.TimeInterval = _IntervalUserInput;

        await SymbolSelectionListHandler(null);
    }

    async Task GetMarketData()
    {
        marketDataList = await MarketDataService.Get(SymbolSelections);
    }

    LineChartDataset<double> GetLineChartDataset(string symbol)
    {
        return new LineChartDataset<double>
        {
            Label = "Symbol: " + symbol,
            Data = marketDataList.Where(x => x.MarketTimestamp.Hour == 0 && x.MarketTimestamp.Minute == 0 && x.symbol.ToLower() == symbol.ToLower() && x.MarketTimestamp > DateTime.Today.AddDays(-TimeInterval)).OrderBy(x => x.MarketTimestamp).Select(x => (double)x.close).ToList(),
            BackgroundColor = backgroundColors,
            BorderColor = borderColors,
            Fill = true,
            PointRadius = 2,
            BorderDash = new List<int>
            { }
        };
    }







#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IMarketDataListService MarketDataService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ISymbolFactsDataListService SymbolListService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
namespace __Blazor.FinanceBro.Pages.HistoricChart
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateAutocomplete_0<TItem>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Collections.Generic.IEnumerable<TItem> __arg0, int __seq1, global::System.Func<TItem, global::System.String> __arg1, int __seq2, global::System.Func<TItem, global::System.Object> __arg2, int __seq3, global::System.Object __arg3, int __seq4, global::Microsoft.AspNetCore.Components.EventCallback<global::System.Object> __arg4, int __seq5, global::System.String __arg5)
        {
        __builder.OpenComponent<global::Blazorise.Components.Autocomplete<TItem>>(seq);
        __builder.AddAttribute(__seq0, "Data", __arg0);
        __builder.AddAttribute(__seq1, "TextField", __arg1);
        __builder.AddAttribute(__seq2, "ValueField", __arg2);
        __builder.AddAttribute(__seq3, "SelectedValue", __arg3);
        __builder.AddAttribute(__seq4, "SelectedValueChanged", __arg4);
        __builder.AddAttribute(__seq5, "Placeholder", __arg5);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
