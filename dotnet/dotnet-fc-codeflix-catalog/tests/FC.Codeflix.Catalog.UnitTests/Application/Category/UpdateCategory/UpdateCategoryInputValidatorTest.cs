using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryInputValidatorTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryInputValidatorTest(UpdateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(DontValidateWhenGuidIsEmpty))]
    [Trait("Application", "UpdateCategoryInputValidatorTest - Use Case")]
    public void DontValidateWhenGuidIsEmpty()
    {
        var input = _fixture.GetValidInput(Guid.Empty);
        var validator = new UpdateCategoryInputValidator();

        var validateResult = validator.Validate(input);

        validateResult.Should().NotBeNull();
        validateResult.IsValid.Should().BeFalse();
        validateResult.Errors.Should().HaveCount(1);
        validateResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
    }

    [Fact(DisplayName = nameof(ValidateWhenIsValid))]
    [Trait("Application", "UpdateCategoryInputValidatorTest - Use Case")]
    public void ValidateWhenIsValid()
    {
        var input = _fixture.GetValidInput();
        var validator = new UpdateCategoryInputValidator();

        var validateResult = validator.Validate(input);

        validateResult.Should().NotBeNull();
        validateResult.IsValid.Should().BeTrue();
        validateResult.Errors.Should().HaveCount(0);
    }
}
