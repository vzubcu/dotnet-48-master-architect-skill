# Test Generation Prompt for .NET 4.8

Generate comprehensive tests for this .NET 4.8 code:

## For NEW Code (TDD approach):
1. Identify all public methods and their contracts.
2. Write failing tests for:
   - Happy path (valid inputs)
   - Boundary conditions (min/max values, empty collections)
   - Invalid inputs (null, empty string, negative numbers)
   - Exception paths (what should throw, what should return Result<T>.Failure)
3. Use NUnit/xUnit with Moq for dependencies.
4. Ensure >80% line coverage, >70% branch coverage.

## For LEGACY Code (Characterization tests):
1. DO NOT modify the production code yet.
2. Write tests that document CURRENT behavior exactly as-is.
3. Include edge cases, even if they seem buggy.
4. Lock behavior with assertions.
5. Only after ALL tests pass, suggest refactor steps.

## Output Format:
- Test class name matching [ClassName]Tests
- Arrange-Act-Assert comments
- Descriptive test names: MethodName_Scenario_ExpectedResult
- Mock setup for all external dependencies
- TransactionScope for integration tests (rollback)
