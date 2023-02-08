import { Component,EventEmitter, Input, AfterViewInit, Output, ViewChild } from '@angular/core';
import { CategorieService } from 'src/app/services/categorie.service';
import { MatTableModule } from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort'
import { LiveAnnouncer } from '@angular/cdk/a11y';
import {MatTableDataSource} from '@angular/material/table';


export interface Produs {
  id: number;
  nume: string;
  pret: number;
}

@Component({
  selector: 'app-produse',
  templateUrl: './produse.component.html',
  styleUrls: ['./produse.component.scss']
})
export class ProduseComponent implements AfterViewInit {

  public listaProduse = [];
  public produse: Produs[] = [];
  public cumpar = [];
  public imgs: string = "";
  public displayedColumns = ['id', "photo", 'nume', 'pret', 'add'];
  public rol:string = "";
  @Input() messageFromParent;
  @Output() messageFromChild = new EventEmitter<string>();

  

  constructor(
    private categorieService: CategorieService,
    private _liveAnnouncer: LiveAnnouncer
  ) { }

  dataSource = new MatTableDataSource(this.listaProduse);

  @ViewChild(MatSort) sort: MatSort;

  ngAfterViewInit() {
    console.log(this.messageFromParent);
    this.AfisCateg();
    this.dataSource.sort = this.sort;
  }
  ngOnInit(): void {
    this.rol = localStorage.getItem("Role");
  }


  public AfisCateg(): void{
    this.categorieService.GetCategories().subscribe(
      (result) => {
        console.log(result);
        
        for (var i = 0; i < result.length; i++)
          {const num = result[i].nume;
           const cod = result[i].idCategorie;
           const but = document.createElement("button");
           but.innerHTML = num;
                     
            but.onclick = () => {this.Buton(cod)};
            
           document.getElementById("lista-categ").appendChild(but);
          }
        
      },
      (error) => {
        console.error(error);
      }
    );
  }


  public Buton(cod): void {
    this.getProd(cod);
  };

    public getProd(cod): void {

      this.categorieService.GetProduse(cod).subscribe(
        (result) => {
          console.log(result);
          this.produse = result;
          switch (cod) {
            case 1:
                this.imgs = "s";
                break;
            case 2:
              this.imgs = "l";
                break;
            case 3:
              this.imgs = "p";
                break;
            case 4:
                this.imgs = "c";
                break;
            case 5:
                this.imgs = "cp";
                break;
        }
        
        },
        (error) => {
          console.log(error);
        }
    
       );
    }
 
  
  public emitData(): void {
    this.messageFromChild.emit('S-a accesat o categorie !');
  }


  announceSortChange(sortState: Sort) {
    
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }

}

public addToCart(element): void {
  
  var n = this.cumpar.length;

   this.cumpar[n] = element;
   console.log(this.cumpar);
 localStorage.setItem("Cumparaturi", JSON.stringify(this.cumpar));
 alert("Produs adaugat cu succes!!");
}

}