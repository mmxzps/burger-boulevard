<script>
import ProductCard from './ProductCard.vue';
export default {
  data() {
    return {
      allProducts: [], // Stores all products fetched from the API
      filteredProducts: [], // Stores filtered products based on category
      categories: [], // Will store categories from the API
      categoryTitles: {
        '/': 'Alla produkter',
        '/hamburgare': 'Hamburgare',
        '/kycklingfisk': 'Kyckling & Fisk',
        '/snacks': 'Snacks',
        '/sidesdip': 'Sides & Dip',
        '/sallad': 'Sallad',
        '/kalldryck': 'Kall Dryck',
        '/varmdryck': 'Varm Dryck',
        '/dessert': 'Dessert',
      },
      categoryIds: {
        '/hamburgare': 1,
        '/kycklingfisk': 2,
        '/snacks': 3,
        '/sidesdip': 4,
        '/sallad': 5,
        '/kalldryck': 6,
        '/varmdryck': 7,
        '/dessert': 8
      }
    };
  },
  computed: {
    currentCategory() {
      return this.$route.path;
    },
    pageTitle() {
      return this.categoryTitles[this.currentCategory] || 'Produkter';
    },
    currentCategoryId() {
      return this.categoryIds[this.currentCategory];
    }
  },
  methods: {
    async fetchCategories() {
      try {
        const response = await fetch('https://localhost:7115/api/Categories');
        this.categories = await response.json();
        console.log('Categories:', this.categories);
      } catch (error) {
        console.error('Error fetching categories:', error);
      }
    },
    async fetchAllProducts() {
      try {
        let url = 'https://localhost:7115/api/Components';
        
        // Handle level 1 categories from the API
        if (this.currentCategoryId >= 1 && this.currentCategoryId <= 8) {
          url = `${url}?level=1&categoryId=${this.currentCategoryId}`;
          console.log("Using category URL:", url);
        }
        
        const response = await fetch(url);
        this.allProducts = await response.json();
        console.log('All products:', this.allProducts);
      } catch (error) {
        console.error('Error fetching all products:', error);
        this.allProducts = []; // Set to empty array on error
      }
    },
    async getApi() {
      try {
        if (this.categories.length === 0) {
          await this.fetchCategories();
        }
        
        await this.fetchAllProducts();
        
        // If the current category is '/', show all products
        if (this.currentCategory === '/') {
          this.filteredProducts = this.allProducts;
        } else {
          // Filter products based on the current category
          this.filteredProducts = this.allProducts;
          
          // Log the filtered products for debugging
          console.log(`Products for category ${this.currentCategory}:`, this.filteredProducts);
        }
      } catch (error) {
        console.error('Error in getApi:', error);
        this.filteredProducts = [];
      }
    }
  },
  watch: {
    // Refetch products when route changes
    '$route'() {
      this.getApi();
    }
  },
  async mounted() {
    // Get categories and all products first
    await this.fetchCategories();
    await this.fetchAllProducts();
    
    // Then filter based on the current route
    this.getApi();
  },
  components: {
    ProductCard
  }
};
</script>

<template>
  <div class="products-container">
    <h1>{{ pageTitle }}</h1>
    <div class="products-grid">
      <div v-for="product in filteredProducts" :key="product.id" class="product-item">
        <ProductCard :burger="product" />
      </div>
    </div>
    <div v-if="filteredProducts.length === 0" class="no-products">
      <p>Inga produkter hittades i denna kategori</p>
    </div>
  </div>
</template>

<style>
.products-container {
  padding: 20px;
  max-width: 1200px;
  margin: 0 auto;
}

h1 {
  color: white;
  font-weight: bold;
  text-align: center;
  margin-bottom: 30px;
}

.products-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  gap: 20px;
}

.product-item {
  display: flex;
  justify-content: center;
}

.no-products {
  text-align: center;
  color: white;
  font-size: 1.2rem;
  margin-top: 50px;
}

@media (max-width: 768px) {
  .products-grid {
    grid-template-columns: repeat(auto-fill, minmax(180px, 1fr));
    gap: 15px;
  }
}
</style>