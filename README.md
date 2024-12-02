# Caesar Cipher Decryption Solution

This is an implementation of the Caesar Cipher decryption system for the RFDS take home interview question built with .NET 8.0.

## Table of Contents
- [Overview](#overview)
- [Getting Started](#getting-started)
- [Architecture](#architecture)
- [Design Decisions](#design)
- [Solution Description](#solution-description)
- [Productionization Proposal](#productionization-proposal)

## Overview

This solution implements a secure implementation of the Caesar Cipher decryption algorithm, where each letter in the encrypted text is shifted 3 positions to reveal the original message.

### Key Features
- Secure memory management
- Input validation
- Error handling
- Logging
- Production Architecture
- Full test coverage

## Getting Started

### Installation
```bash
# Clone the repository
git clone https://github.com/ryanbrooker/RFDSTakeHomeProblem.git

# Navigate to the project directory
cd RFDSTakeHomeProblem

# Build the solution
dotnet build

# Run tests
dotnet test

# Run the application
dotnet run --project RFDSTakeHomeProblem
```

### Usage
```bash
# Example usage through console
> Enter the encrypted text (lowercase letters only):
> defghijk
< Decrypted text: abcdefgh
```

## Architecture

### Project Structure
- **RFDSTakeHomeProblem**: Core business logic and interfaces
- **RFDSTakeHomeProblem.Tests**: xUnit tests

### Design

#### Assumptions
- Input is lowercase letters only
- Maximum input length of 1000 characters
- Synchronous processing is acceptable
- In-memory processing is sufficient


## Solution Description 
This solution implements a console based application that decrypts messages encrypted using the Caesar Cipher algorithm in O(2n  + c) time complexity or O(n) for short. 
Users interact with the application through a command-line interface where they input encrypted text. 
The application processes this input through a decryption service that shifts each letter by three positions in the alphabet to reveal the original message. 
The implementation uses a layered architecture with dependency injection allowing for easy modification and extension. 
Added logging to track operations, while error handling ensures graceful failure scenarios. 
Security measures include input validation, memory protection (clearing sensitive data), and maximum input length restrictions. 
The solution uses .NET 8.0's and implements xUnit tests to ensure the solution works as expected.


## Productionization Proposal

To transform this console application into a production ready service that can be securely used by various users and systems, I would propose the following:

1. **Service Layer Transformation**:
   - Implement a RESTful Web API using ASP.NET Core to allow for web use

2. **Security Infrastructure**:
   - Deploy behind Azure Application Gateway for WAF protection
   - Implement rate limiting and DDoS protection

3. **Scalability and Reliability**:
   - Containerize the application using Docker
   - Deploy to Azure Kubernetes Service (AKS) for orchestration
   - Implement horizontal auto-scaling based on load

4. **Monitoring and Support**:
   - Integrate Application Insights for telemetry
   - API documentation using Swagger/OpenAPI

5. **DevOps Integration**:
   - Establish CI/CD pipelines using Azure DevOps
   - Set up automated testing for deployment

## License

This project is licensed under the MIT License.