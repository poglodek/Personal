**Cursor Rules**

- If a Cursor rules directory exists at `.cursor/rules/` or a top-level `.cursorrules`, follow those precisely.
- Apply Cursor rules before making edits or automated changes; note any overrides at the top of this file.
- When in doubt, defer to Cursor rules and document deviations in a short note.

**Copilot Rules**

- If a Copilot guidance file exists at `.github/copilot-instructions.md`, follow those directions for code generation and edits.
- Treat Copilot guidance as advisory; always validate changes locally before applying them.

**1) Build / Lint / Test**

- Environment assumptions
  - Runtime: .NET (SDK 7.x or 8.x as defined by the repo)
  - Package Manager: dotnet CLI
  - Optional: Roslyn analyzers and StyleCop.Analyzers configured in the project

- Install / Restore
  - `dotnet restore`

- Build
  - `dotnet build -c Release -nologo`
  - Optional: treat warnings as errors: `dotnet build -c Release -nologo /warnaserror`

- Lint / Format / Analyze
  - Formatting: `dotnet format` (ensure formatting tool is installed; if not, install via `dotnet tool install -g dotnet-format`)
  - Static analysis: rely on Roslyn analyzers configured in the project; ensure TreatWarningsAsErrors is enabled in the project.
  - EditorConfig: ensure repository contains an `.editorconfig` aligned to project standards

- Test
  - `dotnet test -v minimal`
  - For release mode tests: `dotnet test -c Release -v minimal`

- Running a single test (quick guide)
  - Use FullyQualifiedName filter:
    - `dotnet test --filter "FullyQualifiedName~Namespace.ClassName.MethodName"`
  - To run all tests in a class:
    - `dotnet test --filter "FullyQualifiedName~Namespace.ClassName"`
  - To run tests in a specific project/assembly:
    - `dotnet test path/to/Your.Tests.csproj --filter "FullyQualifiedName~Namespace.ClassName.MethodName"`

- Optional test tooling
  - Code coverage with Coverlet: `dotnet test /p:CollectCoverage=true /p:CoverletOutput=./coverage/ /p:CoverletOutputFormat=lcov`

**2) Running a Single Test (Concrete Examples)**

- xUnit / NUnit / MSTest (generic)
  - `dotnet test --filter "FullyQualifiedName~MyApp.Tests.CalculatorTests.Add_TwoNumbers"`
- All tests in a class
  - `dotnet test --filter "FullyQualifiedName~MyApp.Tests.CalculatorTests"`
- Specific test project
  - `dotnet test MyApp.Tests/MyApp.Tests.csproj --filter "FullyQualifiedName~CalculatorTests.Subtract_Simple"`

**3) Code Style Guidelines**

- General
  - Prioritize readability over cleverness; choose meaningful, descriptive names.
  - Public APIs must be documented with XML doc comments.
  - Tests should clearly exercise intent; names should reflect behavior.

- Names and conventions
  - Types: PascalCase (e.g., CalculatorService)
  - Methods: PascalCase (e.g., CalculateTotal)
  - Variables/fields: camelCase; private fields often prefixed with `_` (e.g., `_total`);
    readonly fields use `_` prefix as well
  - Namespaces: PascalCase with domain boundaries (e.g., Company.Product.Module)
  - Constants: PascalCase or ALL_CAPS depending on project convention; pick one and stick with it

- Usings and imports
  - Use explicit imports; avoid wildcard/namespace-wide imports
  - Group using directives: System/usings first, then third-party, then project-local
  - In C# 10+ with global usings, prefer minimal per-file usings

- Nullability and types
  - Enable nullable reference types (enable in csproj)
  - Prefer explicit types; use `var` when the type is obvious from the right-hand side
  - Use non-nullable reference types by default; annotate as needed if nullable

- Async programming
  - Prefer async all the way for IO-bound work; ensure `ConfigureAwait(false)` in library code
  - Use `Task`-based async methods; avoid mixing sync and async in public API

- Error handling
  - Do not catch general exceptions; catch specific exceptions
  - Preserve meaningful exception context; wrap with custom domain exceptions where appropriate
  - Avoid swallowing exceptions; rethrow with added context when necessary

- Logging
  - Use `ILogger<T>` for cross-cutting concerns
  - Log at appropriate levels (Information, Warning, Error); avoid noisy logs in success paths
  - Do not log sensitive data; mask secrets

- Formatting and tooling
  - Enforce formatting via `dotnet format` and EditorConfig
  - Maintain consistent line lengths, spacing, and braces style
  - Run formatting checks in CI; add pre-commit hooks if feasible

- Documentation
  - XML docs for public members; summary, params, returns, and exceptions
  - Document architectural decisions and public APIs

- Tests
  - Deterministic, fast, isolated tests; avoid flaky tests
  - Use descriptive test names and inline data when appropriate
  - Include tests for edge cases and failure modes

- Security
  - Validate inputs; avoid leaking sensitive data in logs
  - Use secure patterns for credentials (e.g., secrets management)

- Accessibility and localization
  - If applicable, consider localization keys and accessible error messages

**4) Repository Conventions**

- Project layout
  - Follow existing convention: e.g., `src/YourProject`, `tests/YourProject.Tests`
  - Maintain consistent solution structure; avoid renaming namespaces or folders without consensus

- EditorConfig
  - Provide a repository-wide `.editorconfig` aligning with C# conventions
  - Use analyzer rules and code style preferences that match the project

- CI / Hooks
  - If the repo uses pre-commit, ensure local hooks pass
  - Mirror CI steps locally where possible (restore, build, format, test)

**5) CI / Validation**

- Local-first validation
  - Build, format, and test all changed projects locally
  - Run single-test commands to verify targeted changes
- Coverage and quality
  - Review static analysis reports; fix any reported issues
  - Ensure tests are green across touched projects

**6) Versioning and Changelogs**

- Document user-impact changes in a changelog if necessary
- Keep concise notes about new agents/tools or changed workflows

**7) Cross-Project Consistency**

- If multi-language components exist, align conventions where possible
- Centralize common patterns (logging, error handling, configuration) to reduce drift

**8) Onboarding and Maintenance**

- Provide quick-start commands to reproduce agent tasks locally
- Include a brief section about running the agent tooling (if applicable) and how to extend it

**9) Minimal Example Snippet**

- XML doc snippet for a public method:
  - `/// <summary>Calculates total.</summary>`
  - `/// <param name="price">Price.</param>`
  - `/// <returns>Total amount.</returns>`
- DTO and mapper example: small, clear, named types

**10) Modifications and Merging Guidance**

- If an existing AGENTS.md exists, merge these recommendations thoughtfully
- Preserve project-specific sections; prefer incremental improvements over large rewrites
- If Cursor or Copilot rules conflict with this draft, resolve by prioritizing repository directives

**What I did**

- Created an AGENTS.md tailored for a .NET app at the repository root.
- Includes explicit build/test commands, single-test strategies, and robust code-style guidance.
- Addresses Cursor and Copilot rules to align automated work with repo directives.

Next steps

- Run the local validations: `dotnet restore`, `dotnet build -c Release`, `dotnet format`, `dotnet test -v minimal` on touched projects.
- If you have Roslyn analyzers or StyleCop configured, ensure they pass locally and CI.
- Review and adjust paths for your actual project structure (src/tests folders, solution file location).
