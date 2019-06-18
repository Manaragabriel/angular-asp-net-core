import {Injectable} from '@angular/core'
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';
import { Lote } from '../_models/Lote';
@Injectable({
    providedIn: 'root'
})
export class EventoService{
    constructor(private http: HttpClient){

    }
    
    get_evento(): Observable<Evento[]>{
        
        return this.http.get<Evento[]>("http://localhost:5000/api/evento",)
    }
    get_evento_tema(tema:string): Observable<Evento>{
        
        return this.http.get<Evento>(`http://localhost:5000/api/evento/gettema/${tema}`)
    }
    get_evento_id(id:number): Observable<Evento>{
        return this.http.get<Evento>(`http://localhost:5000/api/evento/${id}`)
    }
    criar_evento(evento: Evento):any{
        return this.http.post("http://localhost:5000/api/evento/",evento,)
    }
    atualizar_evento(evento:Evento,id:number):any{
        
        return this.http.put(`http://localhost:5000/api/evento/${id}`,evento)
    }
    deleta(id:number):any{
        return this.http.delete(`http://localhost:5000/api/evento/${id}`)
    }

    upload_image(file:File):any{
        const fileUp= <File>file[0]
        const formData= new FormData()
        formData.append('file',fileUp,fileUp.name)
        
        return this.http.post(`http://localhost:5000/api/evento/upload`,formData)
    }

    criar_lote(lote:Lote):any{
        return this.http.post(`http://localhost:5000/api/lote`,lote)
    }
}