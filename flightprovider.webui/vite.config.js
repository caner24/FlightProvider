import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    devServer: {
        port: 5173,
        allowedHosts: "all",
        proxy: [
            {
                context: ["/api"],
                target:
                    process.env.services__flightproviderapi__https__0 ||
                    process.env.services__flightproviderapi__http__0,
                pathRewrite: { "^/api": "" },
                secure: false,
            },
        ],
        
    }
})
