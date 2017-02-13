using FluentValidation;
using JsonValidationTest.Representation;

namespace JsonValidationTest.Fluent
{
  internal class SnapshotCsvExportValidator : AbstractValidator<SnapshotCsvExport>
  {
    public SnapshotCsvExportValidator()
    {
      RuleFor(s => s.Culture).NotNull();
      RuleFor(s => s.WriteIndexFile).NotNull();
    }
  }
}
