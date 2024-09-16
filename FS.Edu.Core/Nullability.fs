module FS.Edu.Nullability

open System

let tmp = null

type Email = Email of string
type Person = {
    Name: string
    Email: Email
}

let email = Email null
//let person = { Name = null; Email = null;  }
let date = DateTime.Parse null 