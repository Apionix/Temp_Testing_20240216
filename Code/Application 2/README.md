## Description
This service functions as a simplistic API that interacts with the User table in a MySQL database

## System information

### System architecture

```mermaid
graph TD;

A[(MySQL)];

B[API]

B --> A;
A --> B;

C((Application));

C --> B;
```
