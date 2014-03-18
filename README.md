Physics
=======

A library for physical computation.

## Features ##
- Free creation of unit systems like the [SI system](http://en.wikipedia.org/wiki/International_System_of_Units)
- Perform calculations with physical quantities while retaining consistent unit results
- Add new units on the fly by declaration or parsing
- Display physical quantities in different units and number formats
- Serialize and deserialize physical quantities

## Usage ##
### Create a new unit system ###
```C#
this.System = UnitSystemFactory.CreateSystem("SI");

//Base units
var m = this.System.AddBaseUnit("m", "metre");
var kg = this.System.AddBaseUnit("kg", "kilogram", true); // flag indicates that kilogram has an inherent prefix
var s = this.System.AddBaseUnit("s", "second");
var A = this.System.AddBaseUnit("A", "ampere");
var K = this.System.AddBaseUnit("K", "kelvin");
var mol = this.System.AddBaseUnit("mol", "mole");
var cd = this.System.AddBaseUnit("cd", "candela");

//Derived units
var Hz = this.System.AddDerivedUnit("Hz", "hertz", s ^ -1);
var N = this.System.AddDerivedUnit("N", "newton", kg * m * (s ^ -2));
var Pa = this.System.AddDerivedUnit("Pa", "pascal", N * (m ^ -2));
var J = this.System.AddDerivedUnit("J", "joule", N * m);
var W = this.System.AddDerivedUnit("W", "watt", J / s);
var C = this.System.AddDerivedUnit("C", "coulomb", s * A);
var V = this.System.AddDerivedUnit("V", "volt", W / A);
var F = this.System.AddDerivedUnit("F", "farad", C / V);
var Ω = this.System.AddDerivedUnit("Ω", "joule", V / A);
var S = this.System.AddDerivedUnit("S", "siemens", A / V);
var Wb = this.System.AddDerivedUnit("Wb", "weber", V * s);
var T = this.System.AddDerivedUnit("T", "tesla", Wb * (s ^ -2));
var H = this.System.AddDerivedUnit("H", "inductance", Wb / A);
var lx = this.System.AddDerivedUnit("lx", "immulinance", (m ^ -2) * cd);
var Sv = this.System.AddDerivedUnit("Sv", "sievert", J / kg);
var kat = this.System.AddDerivedUnit("kat", "katal", (s ^ -1) * mol);

//Incoherent units
var h = this.System.AddDerivedUnit("h", "hour", 60 * 60 * s);
```

### Perform calculations ###

```C#
var kWh = UnitPrefix.k * W * h;
var m3 = m ^ 3;

// 100 kWh
var energy = new Quantity(100, kWh);

// 5 m³
var volume = new Quantity(5, m3);

// 20 kWh/m³
var result = energy / volume;
var expected = new Quantity(20, kWh / m3);

Assert.AreEqual(expected, result);
```

### Display results ###

```C#
var quantity = new Quantity(10 * 1000 * 1000, J);

var kWh = this.System.AddDerivedUnit("kWh", "kilowatt hour", UnitPrefix.k * W * h);
string display;

display = quantity.ToString();
Assert.AreEqual("10000000 J", display);

display = quantity.ToString(kWh);
Assert.AreEqual("2.77777777777778 kWh", display);

display = quantity.ToString("N3", kWh);
Assert.AreEqual("2.778 kWh", display);
```

### Serialization ###

```C#
var quantity = new Quantity(100, J / (m ^ 3));

var info = quantity.ToInfo();

var serializer = new JavaScriptSerializer();
var json = serializer.Serialize(info); // {"Amount":100,"Dimension":[-1,1,-2]}
var deserializedInfo = serializer.Deserialize<QuantityInfo>(json);

var result = this.System.FromInfo(deserializedInfo);

Assert.AreEqual(quantity, result);
```
