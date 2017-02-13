using FluentValidation;
using JsonValidationTest.Representation;

namespace JsonValidationTest.Fluent
{
  internal class SnapshotExportValidator : AbstractValidator<SnapshotExport>
  {
    public SnapshotExportValidator()
    {
      RuleFor(s => s.Csv).NotNull();
      RuleFor(s => s.OutputPath).NotEmpty();
      RuleFor(s => s.Parameters).NotNull();
			RuleFor(s => s.Csv).SetValidator(new SnapshotCsvExportValidator());
			RuleFor(s => s.Parameters).SetCollectionValidator(new ParameterValidator());
    }
  }
}
