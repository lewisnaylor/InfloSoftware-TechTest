import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import BootstrapVue from 'bootstrap-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
        vue(),
        BootstrapVue
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  }
})
