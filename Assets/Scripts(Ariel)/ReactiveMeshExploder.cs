using UnityEngine;

namespace SBS.ME // Usamos el mismo namespace para que funcione correctamente
{
    // Esta clase hereda toda la funcionalidad de MeshExploder
    public class ReactiveMeshExploder : MeshExploder
    {
        private bool haExplotado = false;

        /// <summary>
        /// Inicia la explosión desde un punto de impacto específico en el mundo.
        /// </summary>
        /// <param name="worldPoint">El punto en coordenadas del mundo donde ocurrió el impacto.</param>
        public void EXPLODE_AtPoint(Vector3 worldPoint)
        {
            // Si ya se activó la explosión, no hacemos nada.
            if (haExplotado) return;

            haExplotado = true;

            // Forzamos el modo de explosión a "offset" para que use un punto personalizado.
            this.explosionOrigin = ExplosionOrigin.offset;

            // Calculamos el 'offset' necesario. El origen de la explosión será
            // la posición del objeto (su pivote) más este vector de desplazamiento.
            // Al hacer que el resultado sea el punto de impacto, la explosión
            // se originará exactamente donde queremos.
            this.ExplosionOffset = worldPoint - this.transform.position;

            // Llamamos a la función EXPLODE() original de la clase base.
            this.EXPLODE();
        }
    }
}