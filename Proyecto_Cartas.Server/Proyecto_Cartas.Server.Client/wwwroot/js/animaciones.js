
        window.animarCarta = {
            atacar: function (nombre) {
                const carta = document.querySelector(`.carta[data-nombre='${nombre}']`);
                if (!carta) return;

                carta.classList.add("atacando");

                setTimeout(() => {
                    carta.classList.remove("atacando");
                }, 300);
            },

            atacada: function (nombre) {
                const carta = document.querySelector(`.carta[data-nombre='${nombre}']`);
                if (!carta) return;

                carta.classList.add("atacada");

                setTimeout(() => {
                    carta.classList.remove("atacada");
                }, 300);
            }
        };
