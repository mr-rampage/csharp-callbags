# :handbag: C# Callbags

This is a proof of concept to demonstrate how to use the Callbag specification in an Object Oriented paradigm. Functional Programming can be mapped directly to Object Oriented programming with some changes, but the underlying philosophy can be preserved. The original intent of this project was to be used as a learning/teaching tool to introduce reactive stream based programming to an organization that consisted predominantly of Object Oriented programmers.

The secondary goal was to explore the Dr. Alan Kay's intent of Object Oriented programming: 

> OOP to me means only messaging, local retention and protection and hiding of state-process, and extreme late-binding of all things.

By that definition, Callbags appear to be a great fit. From the specification, Callbags are basically passing messages from Callbag to Callbag. The messages are simple: Greet, Deliver, and Terminate.

# Resources

* [Callbag Specification](https://github.com/callbag/callbag)
* [Callbag HowTo Guide](https://github.com/callbag/callbag/blob/master/getting-started.md)
* [Why we need Callbags](https://staltz.com/why-we-need-callbags.html)
