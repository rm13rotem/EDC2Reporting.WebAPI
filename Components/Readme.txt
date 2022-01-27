Components DLL 

Introduction - 
1. Mission statement of the DLL - Components are the building blocks of every 
CRF page. So each component is like a form input with its label.
2. A good example would be the InclusionExclusion CRF page (belonging to the screening visit)
It would have say 3 inclusion questions, which must all be answered YES to be eligible to 
partake in the experiment. It would have say 5 exclusion creteria questions, which must all
be answered NO to be eligible to partake in the experiment. So all in all, in this case we would 
have 8 components, each with it's own label and default value. the components are similar (they
are all YES/NO selection boxes).
3. A component can also be defined by its properties - 
	Id (int), e.g. 1,2,3.... PK and unique
	ComponentType, an enum (text, range, yes/no, select, etc)
    GuidId (for regulatory reasons)
    IsDeleted - true/false - for depricating a component when no longer relevant
    Name - string. e.g. InclusionCreteria1
    Label - string. what comes before the component, e.g. "Are you over the age of 18?"
    OrderId int - seriale order inside a module/form 1,2,3...
    SerializedValue - the actual value of the component at any given time
    DefaultValue - the default value when the component is initialized, or when the CRF is loaded empty
4. values can be simple text values like "3" or "true", or complex values like a json object (in string)

Documentation - 
v 1.0.0.15 - begin documentation.

  