import {Lote} from "./Lote"
import {Palestrante} from "./Palestrante"
import {RedeSociais} from "./RedeSocial"

export interface Evento{
    id: number
    local:string
    nome:string
    imagem:string
    data: Date
    lotes: Lote[]
    RedeSociais: RedeSociais[]
    palestrante_evento:Palestrante[] 
 
}