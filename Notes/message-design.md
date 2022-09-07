# Some Message Design Notes

> The Holy Text of all This: https://queue.acm.org/detail.cfm?id=3415014



## Integration Patterns

### Messaging

### RPCs
One service calling another service.

Pros:
    - Easy to reason about. 
    - You have to do it (like logging in etc)
    - Sometimes we *think* we have to do it to re-establish the sense of "now". 
Cons: 
    - Cause a lot of coupling (no confidence in deploying)
    - Reliability - network issues, servers go down, etc.
    - Performance is hard to normalize.

### Shared Databases

    -  Monoliths to Microservices - Sam Newman
    
### REST (not really used that much but here for completeness)