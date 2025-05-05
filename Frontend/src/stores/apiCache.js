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
        this._categories = (await api.getCategories()).data
      return this._categories
    },

    async components() {
      if (this._components === null)
        this._components = (await api.getComponents()).data
      return this._components
    }
  }
})
