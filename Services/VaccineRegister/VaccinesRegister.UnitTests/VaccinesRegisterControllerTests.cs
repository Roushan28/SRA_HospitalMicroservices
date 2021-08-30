using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaccinesRegister.API.Controllers;
using VaccinesRegister.API.Dtos;
using VaccinesRegister.API.Entites;
using VaccinesRegister.API.Repositories;
using Xunit;

namespace VaccinesRegister.UnitTests
{

    public class VaccinesRegisterControllerTests
    {
        private readonly Mock<IVaccinesRegisterRepository> repositoryStub = new();

        private readonly Random rand = new();

        [Fact]
        public async Task GetRegisteredDetailAsync_WithUnexistingDetail_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetRegisteredDetailAsync(It.IsAny<Guid>()))
                .ReturnsAsync((VaccineRegister)null);

            var controller = new VaccinesRegisterController(repositoryStub.Object);

            // Act
            var result = await controller.GetRegisteredDetailAsync(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetRegisteredDetailAsync_WithExistingDetail_ReturnsExpectedDetail()
        {
            // Arrange
            VaccineRegister expectedDetail = CreateRandomDetail();

            repositoryStub.Setup(repo => repo.GetRegisteredDetailAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedDetail);

            var controller = new VaccinesRegisterController(repositoryStub.Object);

            // Act
            var result = await controller.GetRegisteredDetailAsync(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(expectedDetail);
        }

        [Fact]
        public async Task GetRegisteredDetailsAsync_WithExistingDetails_ReturnsAllDetails()
        {
            // Arrange
            var expectedDetails = new[] { CreateRandomDetail(), CreateRandomDetail(), CreateRandomDetail() };

            repositoryStub.Setup(repo => repo.GetRegisteredDetailsAsync())
                .ReturnsAsync(expectedDetails);

            var controller = new VaccinesRegisterController(repositoryStub.Object);

            // Act
            var actualDetails = await controller.GetRegisteredDetailsAsync();

            // Assert
            actualDetails.Should().BeEquivalentTo(expectedDetails);
        }

        [Fact]
        public async Task GetRegisteredDetailsAsync_WithMatchingDetails_ReturnsMatchingDetails()
        {
            // Arrange
            var allDetails = new[]
            {
                new VaccineRegister(){ Name = "XYZ"},
                new VaccineRegister(){ Name = "AAA"},
                new VaccineRegister(){ Name = "BBB"}
            };

            var nameToMatch = "BBB";

            repositoryStub.Setup(repo => repo.GetRegisteredDetailsAsync())
                .ReturnsAsync(allDetails);

            var controller = new VaccinesRegisterController(repositoryStub.Object);

            // Act
            IEnumerable<RegisteredDetailDto> foundDetails = await controller.GetRegisteredDetailsAsync(nameToMatch);

            // Assert
            foundDetails.Should().OnlyContain(
                    Detail => Detail.Name == allDetails[0].Name || Detail.Name == allDetails[2].Name
                );
        }

        [Fact]
        public async Task CreateDetailAsync_WithDetailToCreate_ReturnsCreatedDetail()
        {
            // Arrange
            var DetailToCreate = new CreateDetailDto(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
               Guid.NewGuid().ToString(),
                DateTime.Now);

            var controller = new VaccinesRegisterController(repositoryStub.Object);

            // Act
            var result = await controller.CreateDetailAsync(DetailToCreate);

            // Assert
            var createdDetail = (result.Result as CreatedAtActionResult).Value as RegisteredDetailDto;
            DetailToCreate.Should().BeEquivalentTo(
                    createdDetail,
                    options => options.ComparingByMembers<RegisteredDetailDto>().ExcludingMissingMembers()
                );
            createdDetail.Id.Should().NotBeEmpty();
            createdDetail.AvailableDate.Should().BeCloseTo(DateTime.Now, DateTime.Now.TimeOfDay);
        }

        [Fact]
        public async Task UpdateDetailAsync_WithExistingDetail_ReturnsNoContent()
        {
            // Arrange
            VaccineRegister existingDetail = CreateRandomDetail();
            repositoryStub.Setup(repo => repo.GetRegisteredDetailAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingDetail);

            var DetailId = existingDetail.Id;
            var DetailToUpdate = new UpdateDetailDto(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                existingDetail.NoOfDepedents + 3,
                DateTime.Now
            );

            var controller = new VaccinesRegisterController(repositoryStub.Object);

            // Act
            var result = await controller.UpdateDetailAsync(DetailId, DetailToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteDetailAsync_WithExistingDetail_ReturnsNoContent()
        {
            // Arrange
            VaccineRegister existingDetail = CreateRandomDetail();
            repositoryStub.Setup(repo => repo.GetRegisteredDetailAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingDetail);

            var controller = new VaccinesRegisterController(repositoryStub.Object);

            // Act
            var result = await controller.DeleteDetailAsync(existingDetail.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        private VaccineRegister CreateRandomDetail()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                NoOfDepedents = "5",
                AvailableDate = DateTime.Now
            };
        }
    }
}
