module FS.Edu.Traversable

open FSharpPlus
open FSharpPlus.Data

type ItemId = ItemId of int
type BasketId = BasketId of int
type CheckoutId = CheckoutId of string

type BasketItem =
    { ItemId: ItemId
      Quantity: float }

type Basket =
    { Id: BasketId;
      Items: BasketItem list }

type ReservedBasketItem =
    { ItemId: ItemId
      Price: float }

type Checkout =
    { Id: CheckoutId
      BasketId: BasketId
      Price: float }
    
type StockError =
    { ItemId: ItemId
      Reason: string }

type ReservationError =
    { Reserved: ReservedBasketItem list
      Reason: string }

type IStock =
   abstract member Reserve : ItemId -> Result<ReservedBasketItem, StockError>
   abstract member Release : ItemId -> Result<BasketItem, StockError>

type Stock() =
    let stock = dict [(ItemId 1, 3); (ItemId 2 , 0); (ItemId 3, 1)]
    
    interface IStock with
        member this.Reserve id =
            match Dict.tryGetValue id stock with
            | None -> Error { ItemId = id; Reason = $"в системе не зарегистрирован товар {id}" }
            | Some count when count < 1 -> Error { ItemId = id; Reason = $"товар {id} закончился" }
            | Some count ->
                stock[id] <- count - 1
                Ok { ItemId = id; Price = 42 }

        member this.Release id =
            match Dict.tryGetValue id stock with
            | None -> Error { ItemId = id; Reason = $"в системе не зарегистрирован товар {id}" }
            | Some count ->
                stock[id] <- count + 1
                Ok { ItemId = id; Quantity = stock[id] }

let stock = Stock() :> IStock

let reserveBasketItem (item: BasketItem) = stock.Reserve item.ItemId

let releaseBasketItem (item: ReservedBasketItem) = stock.Release item.ItemId

module Result =
    let apply a f =
        match f, a with
        | Ok g, Ok x -> g x |> Ok
        | Error e, Ok _ -> Error e
        | Ok _, Error e -> Error e
        | Error e, Error _ -> Error e
    
let rec sequence list =
    let cons head tail = head :: tail

    match list with
    | head :: tail -> Ok cons |> Result.apply head |> Result.apply (sequence tail)
    | [] -> Ok []

let rec traverse f list =
    let cons head tail = head :: tail

    match list with
    | head :: tail -> Ok cons |> Result.apply (f head) |> Result.apply (traverse f tail)
    | [] -> Ok []
    
let traverseF f list =    
    let cons head tail = head :: tail
    
    let initState = Ok []
    let folder item accumulator = Ok cons <*> (f item) <*> accumulator
    
    List.foldBack folder list initState
    
let createCheckout basket =
    let reservedItems =
        basket.Items
        |> traverseF reserveBasketItem

    reservedItems
    |> Result.map
        (fun items ->
            { Id = CheckoutId "some-checkout-id"
              BasketId = basket.Id
              Price = items |> Seq.sumBy (fun x -> x.Price) })

let specialTraverse (f: BasketItem -> Result<ReservedBasketItem, StockError>) list =    
    let initState = Ok []
    
    let folder item accumulator =
        match (f item), accumulator with
        | Ok h, Ok t -> Ok (h :: t)
        | Error e, Ok reserved -> Error { Reserved = reserved; Reason = e.Reason}
        | _, Error e -> Error e
    
    List.foldBack folder list initState

let specialCheckout basket =
    let reservedItems =
        basket.Items
        |> specialTraverse reserveBasketItem
    
    let createCheckout (items : ReservedBasketItem list) =
        { Id = CheckoutId "some-checkout-id"
          BasketId = basket.Id
          Price = items |> Seq.sumBy (fun x -> x.Price) }

    Result.map createCheckout reservedItems
