import { Component, OnInit } from '@angular/core';  
import { FormBuilder, Validators } from '@angular/forms';  
import { Observable } from 'rxjs';  
import { ProductService } from '../product.service';  
import { Product } from '../product';  
  
@Component({  
  selector: 'app-product',  
  templateUrl: './product.component.html',  
  styleUrls: ['./product.component.css']  
})  
export class ProductComponent implements OnInit {  
  dataSaved = false;  
  productForm: any;  
  allProducts: Observable<Product[]>;  
  productIdUpdate = null;  
  massage = null;  
  
  constructor(private formbulider: FormBuilder, private productService:ProductService) { }  
  
  ngOnInit() {  
    this.productForm = this.formbulider.group({  
      Name: ['', [Validators.required]],  
      ProductNumber: ['', [Validators.required]],  
      Color: ['', [Validators.required]],  
      StandardCost: ['', [Validators.required]],  
      ListPrice: ['', [Validators.required]],  
    });  
    this.loadAllProducts();  
  }  
  loadAllProducts() {  
    this.allProducts = this.productService.getAllProduct();  
  }  
  onFormSubmit() {  
    this.dataSaved = false;  
    const product = this.productForm.value;  
    this.CreateProduct(product);  
    this.productForm.reset();  
  }  
  loadProductToEdit(productId: string) {  
    this.productService.getProductById(productId).subscribe(product=> {  
      this.massage = null;  
      this.dataSaved = false;  
      this.productIdUpdate = product.ProductID;  
      this.productForm.controls['Name'].setValue(product.Name);  
      this.productForm.controls['ProductNumber'].setValue(product.ProductNumber);  
      this.productForm.controls['Color'].setValue(product.Color);  
      this.productForm.controls['StandardCost'].setValue(product.StandardCost);  
      this.productForm.controls['ListPrice'].setValue(product.ListPrice);  
    });  
  
  }  
  CreateProduct(product: Product) {  
    if (this.productIdUpdate == null) {  
      this.productService.createProduct(product).subscribe(  
        () => {  
          this.dataSaved = true;  
          this.massage = 'Record saved Successfully';  
          this.loadAllProducts();  
          this.productIdUpdate = null;  
          this.productForm.reset();  
        }  
      );  
    } else {  
      product.ProductID = this.productIdUpdate;  
      this.productService.updateProduct(product).subscribe(() => {  
        this.dataSaved = true;  
        this.massage = 'Record Updated Successfully';  
        this.loadAllProducts();  
        this.productIdUpdate = null;  
        this.productForm.reset();  
      });  
    }  
  }   
  deleteProduct(productId: string) {  
    if (confirm("Are you sure you want to delete this ?")) {   
    this.productService.deleteProductById(productId).subscribe(() => {  
      this.dataSaved = true;  
      this.massage = 'Record Deleted Succefully';  
      this.loadAllProducts();  
      this.productIdUpdate = null;  
      this.productForm.reset();  
  
    });  
  }  
}  
  resetForm() {  
    this.productForm.reset();  
    this.massage = null;  
    this.dataSaved = false;  
  }  
}  