# Feature Switches

This model demonstrates separation between:

- _Switches_ - that can different states that can stored or serialized, and
- Business _decisions_ that are based on the state of one or more switches

To understand what's going on start by looking at the _Model_ classes that describe these abstract concepts, and introduce
a base _BooleanSwitch_ type.

Then, take a look at the concrete implementation, where we need to decide whether to allow _topic comments_ based on:

- Support for topics, and
- Support for comments

In this implementation we chose to serialize switch states in a config file.
