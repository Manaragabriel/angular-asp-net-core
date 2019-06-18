export interface Lote{
    id:number
    nome_lote:string
    preco: number
    quantidade:number
    inicio?: Date
    fim?: Date
    eventoid: number
}