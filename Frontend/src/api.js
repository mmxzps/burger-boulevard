import axios from 'axios'

export const baseUrl = import.meta.env.VITE_API_BASE_URL
export const apiBaseUrl = `${baseUrl}/api`

const client = axios.create({
  baseURL: apiBaseUrl
})

export const getCategories = () =>
  client.get('categories')

export const getComponents = (level, categoryId) =>
  client.get('components', { params: { level, categoryId } })

export const getComponentPolicies = (componentId) =>
  client.get(`components/${componentId}/policies`)

export const getBurgerMenus = () =>
  client.get('components', { params: { level: 2 } })
