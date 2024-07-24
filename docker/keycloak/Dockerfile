FROM registry.access.redhat.com/ubi9 AS ubi-micro-build
# RUN curl -1sLf 'https://dl.cloudsmith.io/public/infisical/infisical-cli/setup.rpm.sh' | sh
RUN curl -1sLf 'https://dl.cloudsmith.io/public/infisical/infisical-cli/setup.rpm.sh' | bash
# RUN yum install -y infisical
# RUN dnf isntall -y https://dl.cloudsmith.io/public/infisical/infisical-cli/rpm/any-distro/any-version/x86_64/infisical_0.26.1_linux_amd64.rpm
RUN mkdir -p /mnt/rootfs
RUN dnf install --installroot /mnt/rootfs infisical --setopt install_weak_deps=false --nodocs -y
# RUN dnf --installroot /mnt/rootfs clean all
# RUN rpm --root /mnt/rootfs -e --nodeps infisical
# RUN cp /mnt/rootfs/usr/bin/infisical /usr/bin/infisical
RUN /mnt/rootfs/usr/bin/infisical

FROM keycloak/keycloak:25.0 AS builder
# Enable health and metrics support
ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true

# Configure a database vendor
ENV KC_DB=postgres

WORKDIR /opt/keycloak
# for demonstration purposes only, please make sure to use proper certificates in production instead
RUN keytool -genkeypair -storepass password -storetype PKCS12 -keyalg RSA -keysize 2048 -dname "CN=server" -alias server -ext "SAN:c=DNS:localhost,IP:127.0.0.1" -keystore conf/server.keystore
RUN /opt/keycloak/bin/kc.sh build

FROM keycloak/keycloak:25.0
COPY --from=builder /opt/keycloak/ /opt/keycloak/
COPY --from=ubi-micro-build /mnt/rootfs /
RUN curl -1sLf 'https://dl.cloudsmith.io/public/infisical/infisical-cli/setup.rpm.sh' | bash
# RUN infisical init
RUN infisical run --projectId=bc3907d3-47cf-4506-b37b-8778ba6e7d50 --env=dev --domain=http://localhost:8080 --comand "echo 'hello'"

# # change these values to point to a running postgres instance
# ENV KC_HOSTNAME=localhost
# ENV KC_DB=postgres
# ENV KC_DB_USERNAME=root
# ENV KC_DB_PASSWORD=
# ENV KC_DB_URL=jdbc:postgresql://localhost:5432/bigpods

# RUN dnf isntall -y https://dl.cloudsmith.io/public/infisical/infisical-cli/rpm/any-distro/any-version/x86_64/infisical_0.26.1_linux_amd64.rpm
# CMD infisical run --projectId=bc3907d3-47cf-4506-b37b-8778ba6e7d50 --env=dev --domain=localhost:8080
# ENTRYPOINT [
#     "infisical", "run",
#     "--projectId", "bc3907d3-47cf-4506-b37b-8778ba6e7d50",
#     "--env", "dev",
#     "--doamin", "localhost:8080",
#     "--",
#     "/opt/keycloak/bin/kc.sh",
#     "start-dev"
# ]
