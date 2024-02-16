# Description
A system upgrade

## Scenario
A system consists of three different applications along with a shared Library system all of which are kept in separate repositories.

To simplify the codebase, a library system created with shared code that is used in all three 
applications to prevent code duplication.

.NET 8 was recently released and management tasks you to upgrade all systems to .NET 8, how do you proceed?

The system can be simplistically represented as below:

```mermaid
graph TD;
A["Library"];
B["Application A"];
C["Application B"];
D["Application C"];

A --> B;
A --> C;
A --> D;

subgraph .NET Standard 2.0
  A
end

subgraph .NET Core 3.1
  B
end

subgraph .NET Framework 4.7
  C
end

subgraph .NET 6
  D
end

```
### Additional information
 - The applications are hosted in the Cloud environment
 - Runs in a serverless/containerised environment
 
 ## Internal guiding questions
 - Compatability
 - Dependencies
 - Infrastructure support
 - Risks
 - Testing?
 
