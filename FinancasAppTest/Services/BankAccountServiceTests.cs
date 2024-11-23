namespace FinancasAppTest.Services;
using System;
using System.Threading.Tasks;
using FinancasApp.Controllers.V1.Dtos.Request;
using FinancasApp.Exceptions;
using FinancasApp.Models;
using FinancasApp.Repositories.Ports;
using FinancasApp.Services;
using Moq;
using Xunit;
public class BankAccountServiceTests
{

    private readonly Mock<IBankAccountRepository> _repositoryMock;
    private readonly BankAccountService _service;

    public BankAccountServiceTests()
    {
        _repositoryMock = new Mock<IBankAccountRepository>();
        _service = new BankAccountService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenBankAccountAlreadyExists()
    {
        // Arrange
        var user = new User { Id = "user-id-123" };
        var request = new CreateBankAccountRequest { Name = "Savings", Balance = 1000 };
        _repositoryMock
            .Setup(repo => repo.BankAccountExistsAsync(user, request.Name))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DuplicatedAccountBankException>(
            () => _service.CreateAsync(user, request));

        Assert.Equal("Do you already have an account with this name!", exception.Message);
        _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<BankAccount>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateBankAccount_WhenBankAccountDoesNotExist()
    {
        // Arrange
        var user = new User { Id = "user-id-123" };
        var request = new CreateBankAccountRequest { Name = "Checking", Balance = 500 };
        _repositoryMock
            .Setup(repo => repo.BankAccountExistsAsync(user, request.Name))
            .ReturnsAsync(false);

        BankAccount capturedBankAccount = null;
        _repositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<BankAccount>()))
            .Callback<BankAccount>(account => capturedBankAccount = account)
            .ReturnsAsync(capturedBankAccount);

        // Act
        var response = await _service.CreateAsync(user, request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Balance, response.Balance);
        Assert.Equal(user.Id, capturedBankAccount.UserId);
        Assert.Equal(request.Name, capturedBankAccount.Name);
        Assert.Equal(request.Balance, capturedBankAccount.Balance);

        _repositoryMock.Verify(repo => repo.BankAccountExistsAsync(user, request.Name), Times.Once);
        _repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<BankAccount>()), Times.Once);
    }
}
