# jsonvalidationtest

Proof of concept for usability (from a software development perspective) and versatility of different validation techniques.  
The data to be validated is input data deserialized from JSON, fed into a REST API (ASP.NET Web API).
  
The three options to be considered are:

1. JSON Schema
2. Validation via Data Annotations
3. Fluent Validation via [Fluent Validation](https://github.com/JeremySkinner/FluentValidation)


#### JSON Schema Validation
I decided not to include JSON Schema Validation because:

Although many of the required validations are possible, this is IMO not a good way to validate input data in a REST API Service. JSON Schema validation must be integrated somehow into ASP.NET Web API. The only approach I could find was to [*subclass* the `JsonMediaTypeFormatter`](http://vitalyal.blogspot.de/2013/10/json-schema-validation-in-web-api.html). It would also be a redundant "island"-approach. An explicit validation of the deserialized input is  present anyway as you **must not** trust user's input. A possible use case for JSON Schema might be the pre-validation of sender generated data which is out of scope of this considerations.

#### Validation via Data Annotations

Data Annotations are the standard ASP.NET MVC validation implementation in .Net Framework in the namespace `System.ComponentModel.DataAnnotations`.

Standard attributes are applied to properties of the model classes (e. g. `Required`, `MaxLength(..)`, `EmailAddress`). Besides `RegularExpression` there is also the possibility to implement custom attributes for specific validations.

In this console project, the actual validation step is invoked explicitly in code.

#### Validation via [Fluent Validation](https://github.com/JeremySkinner/FluentValidation)

A small validation library for .NET that uses a fluent interface and lambda expressions for building validation rules, licensed under the *Apache 2.0* license. 

*Validator*-Types implement rules for different validation scenarios which are targeted to explicit model types. *Validators* are implemented separately from the models. Inside the *Validator*, *Rules* or *RuleGroups* are fluently declared using standard or custom property validators.

FluentValidation can be [easily integrated into ASP.NET Web API](https://www.exceptionnotfound.net/custom-validation-in-asp-net-web-api-with-fluentvalidation/).

## The validations

In the POC is separated into 2 types of scenarios:

- Validation of acutal 'real-life' business models
- Validation of a test class to demonstrate some of the possibilities of *Fluent Validation*

#### Business Model Validation Scenario

Given a business model hierarchy:

- SnapshotExport
  - Csv (Model type: SnapshotCsvExport)
  - List/Array of Parameters (Model type: Parameter)
    - Inside Parameter a List/Array of Parameter Values (Model Type: ParameterValue)
    
The business model properties are tagged with **DataAnnotations**. Also defined are synonymous **FluentValidation** *Validators* in the namespace `JsonValidationTest.Fluent` (e. g. `SnapshotCsvExportValidator`).

A set of more different JSON-Files representing the business models are provided containing valid or invalid data or wrong JSON format.

All JSON files are deserialized and passed through (first) a validation with **DataAnnotations** and (second) with **FluentValidation**.

The scenarios show that both validation techniques are well capable of handling the requirements of validating the incoming business model graph. 

To see the results, download, compile and execute the project. The console output logs the deserialization and validation resuls for each file processed.

It is important to note that even when an embedded object is wrongly JSON-formatted, the embedding object is present and validated. The deserialization is configured in the way that it logs format errors and continues deserialization. This might be different in production implementations.

#### Test Class Validation Scenario

A (simple) test class contains a set of properties which are validated by fluent `TestClassValidator` to show the specific versatility of FluentValidation.

A valid and an invalid JSON-file are deserialized and validated.

To see the specific validations, we take a look at the self-explanatory code of the `TestClassValidator`:

```csharp
public class TestClassValidator : AbstractValidator<TestClass>
{
  public TestClassValidator()
  {
      // Not null
      RuleFor(t => t.NonNullableText).NotNull();

      // Not empty
      RuleFor(t => t.NotEmptyText).NotEmpty();

      // Length between 1 and 10
      RuleFor(t => t.MinLenText).Length(1, 10);

      // Option must be same value as ValidOption (but only if MustBeValidOption is true)
      RuleFor(t => t.Option).Equal(t => t.ValidOption).When(t => t.MustBeValidOption);

      // List must contain at least 2 items (applicable to all Lists/Arrays)
      RuleFor(t => t.TextItems).NotNull().MustContainAtLeast(2);

      // Date must arrive as UTC (ISO with "Z")
      RuleFor(t => t.UtcDate).NotNull().MustBeUtc();
  }
}
```

## Conclusion

#### DataAnnotations

- Support most generic validation requirements 
- Capable of checking wether a date value is UTC or specific checks via custom validation attributes
- Support specifically provided validation error messages
- Glued to the specific model type
- Not clear in which order the Validation Attributes are applied
- Complicated to implement combined validations (which might access other properties of the validated instance). Context to actually validated type is not present (as in type specific *Fluent Validator*)

#### FluentValidation

- Supports most generic validation requirements 
- Capable of checking wether a date value is UTC or specific checks via custom validation attributes
- Supports specifically provided validation error messages
- Clear separation of model type and validation. So it is possible to implement different *Validators* on the same model to support different business validation requirements (which also could be achieved using the concept of *Validation Groups*)
- Each property validator can be designed to be responsible for a single validation (e. g. `ListMustContainAtLeastValidator` validates exactly one constraint, but not non nullability. So it can be applied to nullable and non-nullable properties (combined with a with NotNull property validator).
- Easy to implement generic validations (e. g. `ListMustContainAtLeastValidator` works for all lists/arrays regardless of the contained type)
- Clear hierarchy of validation flow is expressed in the order of fluent configuration
- Context of *Validator* enables implementation of combined validation considering the values of other properties of the validated instance (or the object graph)
