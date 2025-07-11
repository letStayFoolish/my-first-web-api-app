﻿using my_first_web_api;

public static class PizzaService
{
  private static List<Pizza> Pizzas { get; }
  // private static int nextId = 4;
  private static int nextId;

  static PizzaService()
  {
    Pizzas = new List<Pizza>
    {
      new Pizza { Id = 1, Name = "Pepperoni", IsGlutenFree = false },
      new Pizza { Id = 2, Name = "Margherita", IsGlutenFree = true },
      new Pizza { Id = 3, Name = "Hawaiian", IsGlutenFree = false }
    };

    nextId = Pizzas.Count + 1;
  }

  public static List<Pizza> GetAll()
  {
    int num = 5;
    Console.WriteLine($"Before: {num}");

    SimpleFunction.Assign(ref num);
    
    Console.WriteLine($"After: {num}");
    
    return Pizzas;
  }
  // public static List<Pizza> GetAll() => Pizzas;

  public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

  public static void Add(Pizza pizza)
  {
    pizza.Id = nextId++;
    Pizzas.Add(pizza);
  }

  public static void Delete(int id)
  {
    var pizza = Get(id);

    if (pizza is null) return;

    Pizzas.Remove(pizza);
  }

  public static void Update(Pizza pizza)
  {
    var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
    
    if (index == -1) return;

    Pizzas[index] = pizza;
  }
}