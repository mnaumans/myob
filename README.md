<h1 align="center">Welcome to Payslip Generator</h1>
<p>
  <a href="#" target="_blank">
    <img alt="Version" src="https://img.shields.io/badge/license-MIT-green">
  </a>
</p>

> A simple Console Application based on SOLID principles and CI Pipeline.

## Prerequisites

- dotnet core >=5.0.0

## Code Coverage

- Currenty set to 5 lines, can be updated via build pipeline YAML.

## Usage

- Run the primary console app named "PayslipGenerator" with following input for the console window
>GenerateMonthlyPayslip "{Employee Name}" {Annual Salary}

{Employee Name} = Any string name
{Annual Salary} = A Numerice figure e.g. 60000

## CI Process
This solution is using Azure Pipelines. On every commit and merge a containerized build pipeline will trigger. 

## Application Architecture
>The solution architecture is based on SOLID Principles where the each and every step of calculation is dealth as a single service, so in case of any client its easier to pick one or all of the services, as interfaces are being used so it's easy to make changes by creating a new inteface that implements the current one, DI is faciliated so that code is testable. Unit tests are added as the initial steps to implement proper TDD.

## Author

👤 **Nauman**