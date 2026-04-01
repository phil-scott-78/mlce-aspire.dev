---
title: Standalone Aspire dashboard
description: How to use the Aspire dashboard standalone.
order: 77
---



The [Aspire dashboard](/dashboard/overview) provides a great UI for viewing telemetry. The dashboard:

- Ships as a container image that can be used with any OpenTelemetry enabled app.
- Can be used standalone, without the rest of Aspire.

## Start the dashboard

The dashboard is started using the Docker command line.

The preceding Docker command:

- Starts a container from the `mcr.microsoft.com/dotnet/aspire-dashboard:latest` image.
- The container exposes three ports:
  - Mapping the dashboard's port `18888` to the host's port `18888`. Port `18888` has the dashboard UI. Navigate to `http://localhost:18888` in the browser to view the dashboard.
  - Mapping the dashboard's OTLP/gRPC port `18889` to the host's port `4317`. Port `4317` receives OpenTelemetry data from apps using [OTLP/gRPC](https://opentelemetry.io/docs/specs/otlp/#otlpgrpc).
  - Mapping the dashboard's OTLP/HTTP port `18890` to the host's port `4318`. Port `4318` receives OpenTelemetry data from apps using [OTLP/HTTP](https://opentelemetry.io/docs/specs/otlp/#otlphttp).

## Login to the dashboard

Data displayed in the dashboard can be sensitive. By default, the dashboard is secured with authentication that requires a token to login.

When the dashboard is run from a standalone container, the login token is printed to the container logs. The logs are displayed in the Docker Desktop user interface on the **Logs** tab for the **aspire-dashboard** container:

<!-- Image: Screenshot of the Aspire dashboard container logs. -->

After copying the highlighted token into the login page, select the *Login* button.

Alternatively, you can obtain the token from the logs by using the `docker` command:

> [!TIP]
> To avoid the login, you can disable the authentication requirement by setting the `ASPIRE_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS` environment variable to `true`. Additional configuration is available, see [Dashboard configuration](/dashboard/configuration).

For more information about logging into the dashboard, see [Dashboard authentication](/dashboard/explore/#dashboard-authentication).

## Explore the dashboard

The dashboard offers a UI for viewing telemetry. Refer to the documentation to explore the telemetry functionality:

- [Structured logs page](/dashboard/explore/#structured-logs-page)
- [Traces page](/dashboard/explore/#traces-page)
- [Metrics page](/dashboard/explore/#metrics-page)

Although there is no restriction on where the dashboard is run, the dashboard is designed as a development and short-term diagnostic tool. The dashboard persists telemetry in-memory which creates some limitations:

- Telemetry is automatically removed if [telemetry limits](configuration#telemetry-limits) are exceeded.
- No telemetry is persisted when the dashboard is restarted.

### Unavailable features when standalone

The dashboard has functionality for viewing Aspire resources. The dashboard resource features are disabled when it is run in standalone mode. To enable the resources UI, [add configuration for a resource service](/dashboard/configuration/#resources).

[GitHub Copilot functionality](/dashboard/copilot) isn't available in the standalone dashboard. Copilot uses a connection to your IDE to communicate with GitHub Copilot and that connection isn't available when the dashboard is in standalone mode.

## Send telemetry to the dashboard

Apps send telemetry to the dashboard using [OpenTelemetry Protocol (OTLP)](https://opentelemetry.io/docs/specs/otlp/). The dashboard must expose a port for receiving OpenTelemetry data, and apps are configured to send data to that address.

A Docker command was shown earlier to [start the dashboard](#start-the-dashboard). It configured the container to receive OpenTelemetry data on port `4317` (gRPC) and port `4318` (HTTP). The OTLP endpoint addresses are `http://localhost:4317` for gRPC and `http://localhost:4318` for HTTP.

### Configure OpenTelemetry SDK

Apps collect and send telemetry using [their language's OpenTelemetry SDK](https://opentelemetry.io/docs/languages/).

Important OpenTelemetry SDK options to configure:

- OTLP endpoint, which should match the dashboard's configuration, for example, `http://localhost:4317` for gRPC or `http://localhost:4318` for HTTP.
- OTLP protocol. The dashboard supports all OTLP protocols:
  - [OTLP/gRPC](https://opentelemetry.io/docs/specs/otlp/#otlpgrpc) (`grpc`)
  - [OTLP/HTTP](https://opentelemetry.io/docs/specs/otlp/#otlphttp) (`http/protobuf` or `http/json`)

To configure applications:

- Use the OpenTelemetry SDK APIs within the application, or
- Start the app with [known environment variables](https://opentelemetry.io/docs/specs/otel/protocol/exporter/#configuration-options):
  - `OTEL_EXPORTER_OTLP_PROTOCOL` with a value of `grpc`, `http/protobuf`, or `http/json`.
  - `OTEL_EXPORTER_OTLP_ENDPOINT` with a value of `http://localhost:4317` for gRPC or `http://localhost:4318` for HTTP.

> [!NOTE]
> Additional configuration may be required to authenticate with the dashboard if you have configured the dashboard to secure the telemetry endpoint. For more information, see [Securing the telemetry endpoint](/dashboard/security-considerations/#secure-telemetry-endpoint).

## Sample

For a sample of using the standalone dashboard, see the [Standalone Aspire dashboard sample app](https://github.com/dotnet/aspire-samples/tree/main/samples/standalone-dashboard).

## Next steps

- [Configure the Aspire dashboard](/dashboard/configuration)
