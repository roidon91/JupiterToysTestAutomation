# Jupiter Toys Test Automation

## Overview
This project is a test automation framework developed for the **Planit Technical Assessment – Automation**. It consists of automated test cases for verifying functionality of the **Jupiter Toys** web application.

**Application Under Test:** [Jupiter Toys](http://jupiter.cloud.planittesting.com)

## Tech Stack
- **Test Automation Tool:** Playwright
- **Programming Language:** C#
- **Assertion Library:** Fluent Assertions
- **Dependency Injection:** xUnit Dependency Injection
- **CI/CD Integration:** GitHub Actions
- **Required Packages:**
  - `Microsoft.Playwright`
  - `FluentAssertions`
  - `Xunit.DependencyInjection`

## Test Cases Automated

### **Test Case 1: Contact Page Error Validation**
1. Navigate to the Contact Page from the Home Page.
2. Click the Submit button without entering any details.
3. Verify that the appropriate error messages are displayed.
4. Populate all mandatory fields.
5. Validate that the error messages disappear upon filling in the required details.

### **Test Case 2: Contact Form Submission**
1. Navigate to the Contact Page.
2. Populate all mandatory fields.
3. Click the Submit button.
4. Verify that a successful submission message is displayed.
5. Execute this test 5 times to confirm a 100% pass rate.

### **Test Case 3: Shopping Cart Validation**
1. Add the following items to the cart:
   - 2 Stuffed Frogs
   - 5 Fluffy Bunnies
   - 3 Valentine Bears
2. Navigate to the Cart Page.
3. Validate that the subtotal for each product is calculated correctly.
4. Verify that the individual product prices match the expected values.
5. Ensure that the total cart amount is the sum of all subtotals.

## Installation & Setup

### **Prerequisites**
- Install the required dependencies:
  ```sh
  dotnet add package Microsoft.Playwright
  dotnet add package FluentAssertions
  dotnet add package Xunit.DependencyInjection
  dotnet restore
  ```
- Ensure you have a supported browser installed (Chrome, Firefox, Edge, etc.).

### **Running Tests**
Run the automated test suite using the following command:
```sh
# Execute Playwright tests
dotnet test
```

## CI/CD Pipeline
This project is configured to run tests using **GitHub Actions**. Every push or pull request triggers the test execution workflow to ensure stability and reliability of the test automation suite.

## Contributing
1. Fork the repository.
2. Create a feature branch (`git checkout -b feature-branch`).
3. Commit changes (`git commit -m "Your message"`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.

## Author
**Roidon Rodrigues**  
GitHub: [@roidon91](https://github.com/roidon91)

## License
This project is licensed under the MIT License.
