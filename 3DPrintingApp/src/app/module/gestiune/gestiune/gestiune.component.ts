import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-gestiune',
  templateUrl: './gestiune.component.html',
  styleUrls: ['./gestiune.component.scss']
})
export class GestiuneComponent implements OnInit {
 public role: string ="";
  constructor(
    private router: Router,
    private dataService: DataService,
  ) { }
  public date = new Date();


  ngOnInit(): void {
    this.role = localStorage.getItem("Rool");
    this.router.navigate(['/gestiune']);  
   
  }

  public logout(): void{
    localStorage.setItem('Role','Anonim');
    this.router.navigate(['/login']);  }
}
