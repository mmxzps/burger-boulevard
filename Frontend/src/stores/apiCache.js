import * as api from '@/api'
import { defineStore } from 'pinia'

export const useApiCacheStore = defineStore('apiCache', {
  state: () => ({
    _categories: null,
    _components: null
  }),

  getters: {
    async categories() {
      if (this._categories === null)
        this._categories = await api.getCategories()
      return this._categories
    },

    async components() {
      if (this._components === null)
        this._components = await api.getComponents()
      return this._components
    }
  }
})
