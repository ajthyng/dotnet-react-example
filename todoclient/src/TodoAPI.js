import axios from 'axios'

export const todoAPI = axios.create({
  baseURL: 'http://localhost:8080/api/todos'
})
