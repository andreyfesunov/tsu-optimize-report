plugins {
    kotlin("multiplatform") version "2.1.10"
    id("com.android.application") version "8.2.2"
}

kotlin {
    androidTarget()

    jvm("desktop")

    wasmJs {
        browser()
    }
    
    sourceSets {
        val commonMain by getting {
            dependencies {
                implementation(kotlin("stdlib"))
            }
        }
        
        val desktopMain by getting {
            dependencies {
                implementation("org.jetbrains.compose.desktop:desktop:1.5.11")
            }
        }
        
        val androidMain by getting {
            dependencies {
                implementation("androidx.activity:activity-compose:1.8.2")
                implementation("androidx.appcompat:appcompat:1.6.1")
            }
        }
        
        val wasmJsMain by getting {
            dependencies {
            }
        }
    }
}

android {
    compileSdk = 34
    defaultConfig {
        namespace = "org.tsu"
        minSdk = 24
        targetSdk = 34
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_21
        targetCompatibility = JavaVersion.VERSION_21
    }
}