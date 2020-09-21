# :handbag: C# Callbags

A Demo implementation of Callbags as a way of understanding the Callbag specification from an OO perspective. This implementation is similiar to Linq, with the exception that standard Functional Programming terminology is used and this works with both pushable and pullable data.

TLDR; The Functional approach is way better, but if you're stuck on an OO project, it's still a great set of interfaces to use!

## How it works

A Source is a source of data. The data can deliver data on request or by notification.

A Sink is a consumer of data. The sink can request data or react to data. A Sink is a terminal for the data pipeline.

An Operator is both a Source and Sink. An Operator can be used to chain a Source to Sink. Chaining is achieved by C# Extension functions similar to C# Linq.
