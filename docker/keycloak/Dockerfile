FROM quay.io/keycloak/keycloak:24.0 AS builder

# Enable health and metrics support
ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true

# Configure a database vendor
ENV KC_DB=postgres

WORKDIR /opt/keycloak
# for demonstration purposes only, please make sure to use proper certificates in production instead
RUN keytool -genkeypair -storepass password -storetype PKCS12 -keyalg RSA -keysize 2048 -dname "CN=server" -alias server -ext "SAN:c=DNS:localhost,IP:127.0.0.1" -keystore conf/server.keystore
RUN /opt/keycloak/bin/kc.sh build

FROM quay.io/keycloak/keycloak:24.0
COPY --from=builder /opt/keycloak/ /opt/keycloak/

# change these values to point to a running postgres instance
ENV KC_HOSTNAME=localhost
ENV KC_DB=postgres
ENV KC_DB_USERNAME=root
ENV KC_DB_PASSWORD=
ENV KC_DB_URL=jdbc:postgresql://localhost:5432/bigpods
ENTRYPOINT ["/opt/keycloak/bin/kc.sh", "start-dev"]
