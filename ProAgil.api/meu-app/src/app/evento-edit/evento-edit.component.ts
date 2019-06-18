import { Component, OnInit, ViewChild ,TemplateRef} from '@angular/core';
import { FormGroup, FormBuilder, Validators,FormControl } from '@angular/forms';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService,defineLocale,BsLocaleService,ptBrLocale,TabsetComponent} from 'ngx-bootstrap';
import { EventoService } from '../_services/evento.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { Lote } from '../_models/Lote';
defineLocale('pt-br',ptBrLocale)
@Component({
  selector: 'app-evento-edit',
  templateUrl: './evento-edit.component.html',
  styleUrls: ['./evento-edit.component.css']
})
export class EventoEditComponent implements OnInit {

  oldImage:string
  registerForm: FormGroup
  modalRef:BsModalRef
  evento: Evento
  file: File
  constructor(    
    private evento_service: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService,
    private router:ActivatedRoute,
    ) { }
 
  ngOnInit() {
    this.validacao()
    this.getEventos()
  }
  openModal(template: any){
    
    this.modalRef= this.modalService.show(template)
  }

  file_change(event){
      if(event.target.files && event.target.files.length){
        this.file= event.target.files
      }
      console.log(this.file[0].name)
  }


  add_lote(){
    var lote= <Lote>this.registerForm.get("lotes").value
    lote.eventoid= +this.router.snapshot.paramMap.get("id")
    delete lote.id
    this.evento_service.criar_lote(lote).subscribe((ret)=>{
      console.log(ret)
    })
  }

  save(){
    if(this.file){
      this.evento_service.upload_image(this.file).subscribe()
      this.evento.imagem= this.file[0].name
    }else{
      this.evento.imagem= this.oldImage
    }
   
    const id= +this.router.snapshot.paramMap.get("id")
    this.evento_service.atualizar_evento(this.evento,id).subscribe()
  }
  validacao(){
    this.registerForm= this.fb.group({
      nome: ['',[
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50),
      ]],
      local: ['',Validators.required],
      data: ['',Validators.required],
      imagem:['',Validators.required],
      lotes: this.fb.group({
        id:['',Validators.required],
        nome_lote: ['',Validators.required],
        preco:['',Validators.required],
        quantidade:['',Validators.required],
        inicio:['',Validators.required],
        fim:['',Validators.required],
      }),
      redeSociais: this.fb.group({}),
    })
  }
  getEventos(){
    const id= +this.router.snapshot.paramMap.get("id")
    this.evento_service.get_evento_id(id).subscribe((event: Evento)=>{
      this.evento= event
      this.oldImage= event.imagem
      console.log(this.evento)
    })
  }
}
