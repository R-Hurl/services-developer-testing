namespace SlugUtils.Tests;

public class GeneratingSlugs
{
    [Theory]
    [InlineData("Excel Goes Boom", "excel-goes-boom")]
    [InlineData("Tacos are delicious", "tacos-are-delicious")]
    [InlineData("   birds   ", "birds")]
    [InlineData("dog % $ cat", "dog-cat")]
    public void CanGenerateSlugs(string input, string expected)
    {
        var slugGenerator = new SlugGenerator();

        string result = slugGenerator.GenerateSlug(input);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Excel Goes Boom", "excel-goes-boom")]
    [InlineData("Excel Goes Boom", "excel-goes-boom-a")]
    public void SlugMustBeUnique(string input, string expected)
    {
        var slugGenerator = new SlugGenerator();

        string result = slugGenerator.GenerateSlug(input);

        Assert.Equal(expected, result);
    }
}