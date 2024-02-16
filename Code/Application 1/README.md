## Description
This service functions as a notification system that checks data against set limits and sends out notifications if the limits are exceeded

## System information

### System flow

```mermaid
graph TD;
A((START));
B((STOP));

C[Get all notifications];

D{{notification}}

E[Get relevant data points for notification]

F[Check datapoints for limit exceptions]

G{Limit exceeded?}

H[Create message entry]

A --> C;
C --> D;
D --> B;

D --> E;
E --> F;
F --> G;
G -- Yes --> H;
H --> D;
G -- No --> D;

```

### System architecture

```mermaid
graph TD;

A[(MySQL)];

B[System]

B --> A;
A --> B;

C((Timer));

C --> B;
```
