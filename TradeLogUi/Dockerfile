FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
COPY . .

ARG app_compilation_date
ARG app_version
ARG ui_version

ENV APP_COMPILATION_DATE=${app_compilation_date}
ENV APP_VERSION=${app_version}
ENV UI_VERSION=${ui_version}

ENTRYPOINT ["dotnet", "TradeLogUi.dll"]
