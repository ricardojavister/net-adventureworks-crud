import { Injectable } from '@angular/core';  
import { HttpClient } from '@angular/common/http';  
import { HttpHeaders } from '@angular/common/http';  
import { Observable } from 'rxjs';  
import { Product } from './product';  
  
 @Injectable({  
  providedIn: 'root'  
})  
  
export class ProductService {  
  url = 'http://localhost:54288/Api/Product';  
  constructor(private http: HttpClient) { }  
  getAllProduct(): Observable<Product[]> {  
    return this.http.get<Product[]>(this.url + '/GetAll');  
  }  
  getProductById(ProductId: string): Observable<Product> {  
    return this.http.get<Product>(this.url + '/GetProductById/' + ProductId);  
  }  
  createProduct(Product: Product): Observable<Product> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.post<Product>(this.url + '/InsertProduct/',  
    Product, httpOptions);  
  }  
  updateProduct(Product: Product): Observable<Product> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.put<Product>(this.url + '/UpdateProduct/',  
    Product, httpOptions);  
  }  
  deleteProductById(Productid: string): Observable<number> {  
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json'}) };  
    return this.http.delete<number>(this.url + '/DeleteProduct?id=' +Productid,  
 httpOptions);  
  }  
} 