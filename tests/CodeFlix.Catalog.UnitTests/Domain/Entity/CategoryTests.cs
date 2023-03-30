using CodeFlix.Catalog.Domain.Entities;
using CodeFlix.Catalog.Domain.Exceptions;
using System.Xml.Linq;

namespace CodeFlix.Catalog.UnitTests.Domain.Entity
{
    public class CategoryTests
    {
        [Fact]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            // Arrange
            var validData = new { Name = "Category Name", Description = "Category Description" };

            // Act
            var datetimeBefore = DateTime.Now;
            var category = new Category(validData.Name, validData.Description);
            var datetimeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid).ToString(), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > datetimeBefore);
            Assert.True(category.CreatedAt < datetimeAfter);
            Assert.True(category.IsActive);
        }

        [Theory]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void Instantiate_With_IsActive(bool isActive)
        {
            // Arrange
            var validData = new { Name = "Category Name", Description = "Category Description", IsActive = isActive };

            // Act

            var datetimeBefore = DateTime.Now;
            var category = new Category(validData.Name, validData.Description, validData.IsActive);
            var datetimeAfter = DateTime.Now;

            // Assert
            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid).ToString(), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > datetimeBefore);
            Assert.True(category.CreatedAt < datetimeAfter);
            Assert.Equal(validData.IsActive, category.IsActive);
        }

        [Theory]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void Should_Throw_EntityValidationException_When_Name_Is_Empty(string? name)
        {
            // Arrange
            var action = () => new Category(name!, "Category description");

            // Act
            var exception = Assert.Throws<EntityValidationException>(action);

            // Assert
            Assert.Equal("Name should not be empty or null", exception.Message);
        }

        [Theory]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("A")]
        [InlineData("AB")]
        public void Should_Throw_EntityValidationException_When_Name_Is_Less_Than_3_Characters(string name)
        {
            // Arrange
            var action = () => new Category(name, "Category description");

            // Act
            var exception = Assert.Throws<EntityValidationException>(action);

            // Assert
            Assert.Equal("Name should be at leats 3 characters long", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Category - Aggregates")]
        public void Should_Throw_EntityValidationException_When_Name_Is_Greater_Than_255_Characters()
        {
            // Arrange
            var name = string.Join(null, Enumerable.Range(1, 256).Select(_ => "A"));

            var action = () => new Category(name, "Category description");

            // Act
            var exception = Assert.Throws<EntityValidationException>(action);

            // Assert
            Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Category - Aggregates")]
        public void Should_Throw_EntityValidationException_When_Description_Is_Null()
        {
            // Arrange
            var action = () => new Category("Category Name", null);

            // Act
            var exception = Assert.Throws<EntityValidationException>(action);

            // Assert
            Assert.Equal("Description should not be null", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Category - Aggregates")]
        public void Should_Throw_EntityValidationException_When_Description_Is_Greater_Than_10000_Characters()
        {
            // Arrange
            var description = string.Join(null, Enumerable.Range(1, 10001).Select(_ => "A"));

            var action = () => new Category("Category name", description);

            // Act
            var exception = Assert.Throws<EntityValidationException>(action);

            // Assert
            Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Category - Aggregates")]
        public void Activate()
        {
            // Arrange
            var validData = new { Name = "Category Name", Description = "Category Description", IsActive = false};

            // Act
            var category = new Category(validData.Name, validData.Description, validData.IsActive);

            category.Activate();

            // Assert
            Assert.True(category.IsActive);
        }

        [Fact]
        [Trait("Domain", "Category - Aggregates")]
        public void Deactivate()
        {
            // Arrange
            var validData = new { Name = "Category Name", Description = "Category Description", IsActive = true };

            // Act
            var category = new Category(validData.Name, validData.Description, validData.IsActive);

            category.Deactivate();

            // Assert
            Assert.False(category.IsActive);
        }

        [Fact]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            // Arrange
            var category = new Category("Category Name", "Category Description");
            var newData = new { Name = "New category name", Description = "new category description" };

            // Act
            category.Update(newData.Name, newData.Description);

            Assert.Equal(newData.Name, category.Name);
            Assert.Equal(newData.Description, category.Description);

        }
    }
}
