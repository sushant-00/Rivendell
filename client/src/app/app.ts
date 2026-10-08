import { Component, signal,inject,OnInit } from '@angular/core';
import{HttpClient , } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';


@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected  member  = signal<any>([]);
  private http = inject(HttpClient);
  protected readonly title = signal('Dating app');

  async ngOnInit() {
    this.member.set(await this.getMembers())

  }

  getMembers(){
    try{
      return lastValueFrom(this.http.get('https://localhost:5001/api/Members'));

    }
    catch(error){
      console.log(error);
      throw error;
    }
  }
}
