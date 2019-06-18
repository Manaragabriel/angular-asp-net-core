import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService,defineLocale,BsLocaleService,ptBrLocale } from 'ngx-bootstrap';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import {ToastrService} from 'ngx-toastr'
defineLocale('pt-br',ptBrLocale)
 
@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  form: FormGroup
  modalRef:BsModalRef
  eventos: Evento[]
  id: number
  evento: Evento
  eventosFiltrados: Evento[]
  altura=50
  largura=50
  imagemEditar:string
  mostraimg=true
  _busca='';
  file:File
  get busca():string{
    return this._busca 
  }
  set busca(value:string){
      this._busca= value;
      this.eventosFiltrados= this.busca? this.filtrar(this.busca): this.eventos

  }
  open_modal(template: any,evento:Evento){
    //this.modalRef= this.modalService.show(template)
    this.imagemEditar=evento.imagem
    var _evento= Object.assign({},evento) 
    _evento.imagem= ''
    this.form.patchValue(_evento)
    this.id= evento.id
    
    template.show()
  }
  editar(){
    if(this.file){
      this.form.value.imagem= <File>this.file[0].name
      this.evento_service.upload_image(this.file).subscribe()
    }else{
      this.form.value.imagem= this.imagemEditar
    }
    
    
    this.evento_service.atualizar_evento(this.form.value,this.id).subscribe((resp)=>{
      console.log(resp)
    }) 
  }
  deleta(id: number){
    this.evento_service.deleta(id).subscribe((resp)=>{
      this.getEventos()
      this.toastr.success("Deletado com sucesso","Ação")
    })
  }
  filtrar(value:string):Evento[]{
    value= value.toLocaleLowerCase()
    return this.eventos.filter(
      evento=> evento.nome.toLocaleLowerCase().indexOf(value) !== -1
    )
  }
  
  salvarAlteracao(){
    this.evento= Object.assign({},this.form.value)
    this.evento_service.upload_image(this.file).subscribe()
    this.evento.imagem= this.evento.imagem.split('\\',3)[2]
    this.evento_service.criar_evento(this.evento).subscribe((resp)=>{this.getEventos()})
   
  }

  alterar_img(){
    this.mostraimg= !this.mostraimg
  }

  constructor(
    private evento_service: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private bl: BsLocaleService,
    private toastr: ToastrService,
  ) { 
    this.bl.use('pt-br')
  }

  ngOnInit() {
    this.validacao()
    this.getEventos()
  
  }
  fileChange(event){
    var reader= new FileReader()
    if(event.target.files && event.target.files.length) {
      this.file= event.target.files
      console.log(this.file)
    }

  }

  validacao(){
    this.form= this.fb.group({
      nome: ['',[
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50),
      ]],
      local: ['',Validators.required],
      data: ['',Validators.required],
      imagem:['',Validators.required],
    })
  }

  getEventos(){
       this.evento_service.get_evento().subscribe((_eventos: Evento[])=>{
         console.log(_eventos)
          this.eventos= _eventos;
          this.eventosFiltrados=_eventos
      },error=>{
          console.log(error)
      })
  }
}
