import {Evento} from "./Evento"
import {RedeSociais} from "./RedeSocial"


export interface Palestrante{
    id: number
    nome: string
    telefone: string
    email: string
    imagem: string
    RedeSociais: RedeSociais[]
    palestrante_evento: Evento[]
   
}