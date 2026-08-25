# IncaTechnologies Recurrence Radzen Components

Blazor components built with [Radzen](https://blazor.radzen.com) for creating, editing, validating, and displaying recurrence definitions from [IncaTechnologies.Recurrence](https://www.nuget.org/packages/IncaTechnologies.Recurrence).

The library provides a form-friendly UI for daily, weekly, monthly, and yearly schedules. It produces the immutable `IRecurrent` hierarchy used by the core recurrence library, so the selected value can be evaluated and persisted by the application without a UI-specific model.

## Features

- Select daily, weekly, monthly, or yearly recurrence patterns.
- Configure times, days of the week, days of the month, and months as appropriate for the selected pattern.
- Bind recurrence values with standard Blazor `@bind-Value` syntax.
- Use the pickers inside `RadzenTemplateForm` components and Radzen validators.
- Render a recurrence as a localized, human-readable sentence with `ToFriendlyString`.
- Use the included English and Italian resource strings, or provide custom text and localization.

## Requirements

- .NET 10 or later.
- A Blazor application configured to use Radzen Blazor components.
- An interactive Blazor render mode for pages that contain the pickers.

## Installation

Install the package from NuGet:

```shell
dotnet add package IncaTechnologies.Recurrence.Radzen --prerelease
```

The package brings in `IncaTechnologies.Recurrence` and `Radzen.Blazor`. If the application does not already use Radzen, complete the [Radzen Blazor installation](https://blazor.radzen.com/get-started) steps to register its services and add its theme and scripts to the host application.

## Configuration

Register Radzen and the recurrence localizer in `Program.cs`. The localizer supplies the built-in UI text and formatted recurrence descriptions.

```csharp
using IncaTechnologies.Recurrence.Radzen;
using Radzen;

builder.Services.AddScoped<ILocalizer, RecurrenceLocalizer>();
builder.Services.AddRadzenComponents();
```

If the application already registers an `ILocalizer`, use an implementation that returns the recurrence resource keys needed by the components, or supply the text parameters documented below.

Add the component namespaces to `_Imports.razor` or to an individual Razor component:

```razor
@using IncaTechnologies.Recurrence
@using IncaTechnologies.Recurrence.Radzen
```

## Usage

Use `RecurrencePicker` when users should be able to choose the recurrence frequency. Bind it to an `IRecurrent?` property:

```razor
<RadzenTemplateForm TItem="ScheduleModel" Data="@model">
	<RecurrencePicker Name="recurrence" @bind-Value="model.Recurrence" />
	<RadzenRequiredValidator Component="recurrence" Text="A recurrence is required." />
</RadzenTemplateForm>

@code {
	private readonly ScheduleModel model = new();

	private sealed class ScheduleModel
	{
		public IRecurrent? Recurrence { get; set; }
	}
}
```

When a user chooses a frequency, `RecurrencePicker` displays the matching specialized picker. Each edit rebuilds the corresponding core recurrence value and raises the normal Blazor `ValueChanged` and Radzen `Change` notifications.

Use the specialized components directly when the frequency is fixed:

| Component | Use case |
| --- | --- |
| `DailyRecurrencePicker` | Configure one or more times each day. |
| `WeeklyRecurrencePicker` | Configure days of the week and optional times. |
| `MonthlyRecurrencePicker` | Configure calendar days or ordinal weekdays in a month. |
| `YearlyRecurrencePicker` | Configure monthly recurrence rules within a year. |

All picker components support `@bind-Value` and inherit Radzen form-component features such as `Name`, `Disabled`, and `Visible`.

### Display a recurrence

Use `ToFriendlyString` to show the current recurrence as readable text:

```razor
@if (model.Recurrence is not null)
{
	<RadzenText Text="@model.Recurrence.ToFriendlyString()" />
}
```

The formatter uses the current UI culture and the registered `ILocalizer` when one is available. Pass a localizer explicitly when formatting outside a component:

```csharp
var description = recurrence.ToFriendlyString(localizer);
```

## Customization and localization

`RecurrencePicker` exposes `EveryText`, `DayText`, `WeekText`, `MonthText`, and `YearText` for overriding its frequency labels. The specialized pickers expose similar text parameters for their buttons and field captions.

`RecurrenceLocalizer` reads the package resource files, which currently include English and Italian strings. To use application-specific translations, register a custom `ILocalizer` that resolves the keys in `RecurrenceStrings` for the active `CultureInfo`.

## How it works

The components are adapters between Radzen controls and the fluent recurrence model provided by `IncaTechnologies.Recurrence`:

1. The user selects a frequency and configures its details in Radzen controls.
2. The relevant picker translates those values into an `IDaily`, `IWeekly`, `IMonthly`, or `IYearly` instance.
3. The picker publishes the root `IRecurrent` value through Blazor binding and Radzen change events.
4. The application can pass that value to the core library to calculate occurrences, store the definition, or format it for display.

## Example application

The repository includes an interactive sample in [`Example.Blazor`](../Example.Blazor) that demonstrates form integration, validation, predefined recurrence values, and formatted output.

## Contributing

Contributions are welcome. Fork the repository, make a focused change with appropriate tests where applicable, and open a pull request. Please keep changes consistent with the existing code style.

## Issues and feature requests

Report bugs or request features through the [GitHub issue tracker](https://github.com/Matt90hz/RecurrentDates/issues). Include the package version, hosting model, reproduction steps, and expected behavior.

## License

This project is distributed under the [MIT License](LICENSE.txt).
