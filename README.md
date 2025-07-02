# FarmConnect
## MY KASMALL PROJECT
FarmConnect is a modern, cross-platform application built with .NET MAUI (.NET 9) designed to connect farmers and clients, promoting sustainable agriculture and fresh produce access. The app features a clean, responsive design with natural, earthy tones to reflect its agricultural focus.
## Day 1
## Features

- **Cross-Platform:** Runs on iOS, Android, Windows, and macOS.
- **Modern Login UI:**  
  - Soft green gradient background inspired by nature    
  - Centered, rounded login card for all devices  
  - Email/Username and Password fields (with visibility toggle)  
  - "Remember Me" option  
  - "Forgot Password?" recovery link  
  - Social login buttons (Google, Facebook)  
  - Sign-up navigation for new users  
- **Responsive Design:** Adapts to mobile, tablet, and desktop screens.
- **Touch-Optimized:** All controls are easy to use on touch devices.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with .NET MAUI workload installed

### Building and Running

1. **Clone the repository:**
2. **Restore dependencies:**
3. **Build and run:**
- For Android:
  ```sh
  dotnet build -t:Run -f net9.0-android
  ```
- For iOS (on macOS):
  ```sh
  dotnet build -t:Run -f net9.0-ios
  ```
- For Windows:
  ```sh
  dotnet build -t:Run -f net9.0-windows10.0.19041.0
  ```
- For macOS:
  ```sh
  dotnet build -t:Run -f net9.0-maccatalyst
  ```

Or use Visual Studio 2022 to select the target platform and run/debug.

## Project Structure

- `FarmConnect/App.xaml` - Application resources and styles
- `FarmConnect/LoginPage.xaml` - Responsive login UI
- `FarmConnect/AppShell.xaml` - Navigation shell

## Customization
- **Colors & Styles:** Adjust in `App.xaml` or directly in the XAML files.
- **Social Login:** Implement platform-specific authentication logic in the ViewModel and code-behind.

## Contributing

Contributions are welcome! Please open issues or submit pull requests for improvements and bug fixes.

## License

This project is licensed under the MIT License.

---

**FarmConnect** – Bringing farmers and clients together for a greener future.
