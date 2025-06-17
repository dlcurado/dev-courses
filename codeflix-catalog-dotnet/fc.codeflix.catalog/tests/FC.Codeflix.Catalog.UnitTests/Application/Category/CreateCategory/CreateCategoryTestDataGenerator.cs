using FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;
public class CreateCategoryTestDataGenerator
{

    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new CreateCategoryTestFixture();
        var invalidInputList = new List<object[]>();
        var totalInvalidCases = 4;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    // Name menor que 3
                    invalidInputList.Add(
                        new object[] {
                            fixture.GetInvalidInputShortName(),
                            "Name should be at least 3 characters long"
                        }
                    );
                    break;
                case 1:
                    // Name maior que 255
                    invalidInputList.Add(
                        new object[] {
                            fixture.GetInvalidInputTooLongName(),
                            "Name should be less or equal 255 characters"
                        }
                    );
                    break;
                case 2:
                    // Description null
                    invalidInputList.Add(
                        new object[] {
                            fixture.GetInvalidInputDescriptionNull(),
                            "Description should not be null"
                        }
                    );
                    break;
                default:
                    // Description maior que 10_000
                    invalidInputList.Add(
                        new object[] {
                            fixture.GetInvalidInputToLongDescription(),
                            "Description should be less or equal 10000 characters"
                        }
                    );
                    break;
            }
        }

        return invalidInputList;
    }
}
